﻿using Asp.Versioning;
using AspNetCoreRateLimit;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>services.AddCors(options => 
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });
        public static void ConfigureIISIntegration(this IServiceCollection services) =>services.Configure<IISOptions>(options =>{});
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services) => services.AddHttpCacheHeaders((expirationOpt) => { expirationOpt.MaxAge = 65; expirationOpt.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private; }, (validationOpt) => { validationOpt.MustRevalidate = true; });
        public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();
        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule> { new RateLimitRule { Endpoint = "*", Limit = 200, Period = "1m" } };
            services.Configure<IpRateLimitOptions>(opt => { opt.GeneralRules = rateLimitRules; });
            services.AddSingleton<IRateLimitCounterStore,
            MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            }).EnableApiVersionBinding().AddMvc();
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AE For Life API",
                    Version = "v1",
                    Description = "AE For Life API by AE",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ahmed Elbaradey",
                        Email = "elbaradeyahmed1985@gmail.com",
                        Url = new Uri("https://linkedin.com/ahmedelbaradey"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "AE For Life  API LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "AE For Life API",
                    Version = "v2",
                    Description = "AE For Life API by AE",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ahmed Elbaradey",
                        Email = "elbaradeyahmed1985@gmail.com",
                        Url = new Uri("https://linkedin.com/ahmedelbaradey"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "AE For Life  API LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
        }

       
    }
}
