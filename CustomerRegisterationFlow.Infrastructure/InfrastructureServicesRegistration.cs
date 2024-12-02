using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Infrastructure.HashedPassword;
using CustomerRegisterationFlow.Infrastructure.OTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerRegisterationFlow.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddTransient<ITOTP, TOTP>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            return services;
        }
    }
}
