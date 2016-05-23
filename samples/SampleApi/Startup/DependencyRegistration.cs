using Digipolis.Toolbox.Eventhandler;
using Microsoft.Extensions.DependencyInjection;
using SampleApi.Business;

namespace SampleApi
{
	public static class DependencyRegistration
	{
		public static IServiceCollection AddBusinessServices(this IServiceCollection services)
		{
            // Register your business services here, e.g. services.AddTransient<IMyService, MyService>();
            services.AddTransient<IEventHandler, EventHandler>();
            services.AddTransient<IMyDemoEntityBL, MyDemoEntityBL>();
            return services;
		}
	}
}