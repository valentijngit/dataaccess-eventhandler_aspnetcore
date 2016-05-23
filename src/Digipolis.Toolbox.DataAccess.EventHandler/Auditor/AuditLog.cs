using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.Toolbox.DataAccess.EventHandler.Auditor;

namespace Digipolis.Toolbox.DataAccess.EventHandler
{
    [Serializable]
    public class AuditLog
    {
        public AuditLog()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
        public string EntityFullName { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string User { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string PropertyName { get; set; }
        public AuditType Operation { get; set; }
    }
}