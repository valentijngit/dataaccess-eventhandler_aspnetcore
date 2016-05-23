using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.Toolbox.DataAccess.EventHandler.Auditor;
using Digipolis.Toolbox.Eventhandler;

namespace Digipolis.Toolbox.DataAccess.EventHandler
{
    public class DgplsEFInterceptor : IDbCommandInterceptor

    {
        public IEventHandler MyEventHandler { get; set; }
        public DgplsEFInterceptor(IEventHandler eventHandler)
        {
            MyEventHandler = eventHandler;

        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            //WriteLog(string.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText));
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {

            // just for update and delete commands
            if (command.CommandText.StartsWith("update", StringComparison.InvariantCultureIgnoreCase) ||
                command.CommandText.StartsWith("delete", StringComparison.InvariantCultureIgnoreCase))
            {
                var context = interceptionContext.DbContexts.First();
                var entries = context.ChangeTracker.Entries().Where(
                    e => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToList();


                var auditor = new DgplsEFAuditor(MyEventHandler);
                foreach (var entry in entries)
                {
                    auditor.ApplyAuditLog(context, entry);
                }


            }

        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            // just for update and delete commands
            if (command.CommandText.StartsWith("insert", StringComparison.InvariantCultureIgnoreCase))
            {
                var context = interceptionContext.DbContexts.First();
                var entries = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();

                var auditor = new DgplsEFAuditor(MyEventHandler);
                foreach (var entry in entries)
                {
                    auditor.ApplyAuditLog(context, entry);
                }
            }


        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            //WriteLog(string.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText));
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //WriteLog(string.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText));
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            // WriteLog(string.Format(" IsAsync: {0}, Command Text: {1}", interceptionContext.IsAsync, command.CommandText));
        }



    }
}