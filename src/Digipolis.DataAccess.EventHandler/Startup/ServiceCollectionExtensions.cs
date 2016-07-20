using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Digipolis.DataAccess.EventHandler
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessEventHandler(this IServiceCollection services)
        {            

            RegisterServices(services);

            return services;
        }



        private static void RegisterServices(IServiceCollection services)
        {          
          

        }


    }



}