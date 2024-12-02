using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.LoggerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerRegisterationFlow.LoggerService
{
    public static class LoggerServiceServicesRegistration
    {
        public static IServiceCollection ConfigureLoggerServices(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddSingleton<ILoggerManager, LoggerManager>();
            return services;
        }
    }
}
