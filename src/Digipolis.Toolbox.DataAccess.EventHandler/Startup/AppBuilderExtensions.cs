using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.Toolbox.Eventhandler;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Digipolis.Toolbox.DataAccess.EventHandler
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseDataAccessEventHandler(this IApplicationBuilder app)
        {
            var EventhandlerService = app.ApplicationServices.GetService<IEventHandler>();
            if (EventhandlerService == null)
            {
                throw new NullReferenceException("EventHandler service not registered.");
            }

            DbInterception.Add(new DgplsEFInterceptor(EventhandlerService));
            //DbInterception.Add(new DgplsEFTreeInterceptor(EventhandlerService));



            return app;
        }
    }
}

