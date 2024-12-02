using CustomerRegisterationFlow.Persistence.Repositories;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace CustomerRegisterationFlow.Persistence
{
    public static class PersistenceServicesRegistration
    {
     //   public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
     //   {
     //       services.AddDbContext<RepositoryContext>(options =>
     //         options.ConfigureWarnings(warnings =>warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
     //         .UseSqlServer(
     //              configuration.GetConnectionString("sqlConnection")));
     ////       services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CC.Infrastructure")));

     //       //        services.AddDbContext<RepositoryContext>(options =>
     //       //options.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
     //       //b => b.MigrationsAssembly(typeof(RepositoryContext).Assembly.FullName)));




     //       services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
     //       services.AddScoped<IUnitOfWork, UnitOfWork>();
     //       services.AddScoped<ICustomerRepository,CustomerRepository>();
     //       return services;
     //   }
      
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMappings();
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        //private static void AddMappings(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //}

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
            .UseSqlServer(configuration.GetConnectionString("sqlConnection"), builder => builder.MigrationsAssembly(typeof(RepositoryContext).Assembly.FullName)));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<ICustomerRepository, CustomerRepository>();
        }
    }
}
