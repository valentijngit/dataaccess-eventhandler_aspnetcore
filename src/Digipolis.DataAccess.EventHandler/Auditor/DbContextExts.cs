using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Digipolis.DataAccess.EventHandler;
using Digipolis.DataAccess.EventHandler.Auditor;

namespace Digipolis.DataAccess.EventHandler
{
    public static class DbContextExts
    {

        /// <summary>
        /// Get's the entity's key
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="context">The current context</param>
        /// <param name="entity">Entity</param>
        /// <returns>The entity key</returns>
        public static EntityKey GetEntityKey<T>(this IObjectContextAdapter context, T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var oc = context.ObjectContext;
            ObjectStateEntry ose;

            return oc.ObjectStateManager.TryGetObjectStateEntry(entity, out ose) ? ose.EntityKey : null;
        }

    }
}