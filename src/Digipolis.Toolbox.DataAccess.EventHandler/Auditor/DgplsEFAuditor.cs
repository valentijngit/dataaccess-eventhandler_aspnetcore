using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Digipolis.Toolbox.Eventhandler;
using Newtonsoft.Json;

namespace Digipolis.Toolbox.DataAccess.EventHandler.Auditor
{
    public class DgplsEFAuditor
    {

        public DgplsEFAuditor(IEventHandler eventHandler)
        {
            MyEventHandler = eventHandler;

        }

        public IEventHandler MyEventHandler { get; set; }


        public void ApplyAuditLog(DbContext workingContext, DbEntityEntry entry)
            {
                AuditType audittype;
                switch (entry.State)
                {
                    case EntityState.Added:
                    audittype = AuditType.Created;
                        break;
                    case EntityState.Deleted:
                    audittype = AuditType.Deleted;
                        break;
                    case EntityState.Modified:
                    audittype = AuditType.Updated;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                ApplyAuditLog(workingContext, entry, audittype);
            }

            public void ApplyAuditLog(DbContext workingContext, DbEntityEntry entry, AuditType auditType)
            {
                var includedProperties = new List<string>();
                var entityKey = workingContext.GetEntityKey(entry.Entity);
                var entityType = entry.Entity.GetType();

                var props = entityType.GetProperties();
                includedProperties.AddRange(props.Select(p => p.Name));
                

                if (entry.State == EntityState.Modified)
                {
                    var originalValues = workingContext.Entry(entry.Entity).GetDatabaseValues();
                    var changedProperties = (from propertyName in originalValues.PropertyNames
                                             let propertyEntry = entry.Property(propertyName)
                                             let currentValue = propertyEntry.CurrentValue
                                             let originalValue = originalValues[propertyName]
                                             where (!Equals(currentValue, originalValue) && includedProperties.Contains(propertyName))
                                             select new ChangedProperty
                                             {
                                                 Name = propertyName,
                                                 CurrentValue = currentValue,
                                                 OriginalValue = originalValue
                                             }).ToArray();

                    if (changedProperties.Any())
                    {
                        foreach (var auditItem in changedProperties.Select(changedProperty => new AuditLog
                        {
                            Created = DateTime.Now,
                            EntityFullName = entry.Entity.GetType().FullName,
                            Entity = JsonConvert.SerializeObject(entry.Entity),
                            EntityId = JsonConvert.SerializeObject(entityKey),
                            Operation = auditType,
                            OldValue = changedProperty.OriginalValue.ToString(),
                            NewValue = changedProperty.CurrentValue.ToString(),
                            PropertyName = changedProperty.Name
                        }))
                        {
                        SendOutEvent(auditItem);
                        }
                        
                    }
                }
                else
                {
                    var auditItem = new AuditLog
                    {
                        Created = DateTime.Now,
                        EntityFullName = entry.Entity.GetType().FullName,
                        Entity = JsonConvert.SerializeObject(entry.Entity),
                        EntityId = JsonConvert.SerializeObject(entityKey),
                        Operation = auditType,
                    };

                  SendOutEvent(auditItem);


            }
        }

        internal void SendOutEvent(AuditLog auditLog)
        {

            string typeofevent = auditLog.EntityFullName + ":" + auditLog.Operation;

            MyEventHandler.Publish<AuditLog>("CUD", typeofevent, auditLog, "MyUserName", "MyIp", "Mycomponbentid", "Mycomponentname");


        }

        }
    }