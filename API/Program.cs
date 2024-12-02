using API.Extensions;
using AspNetCoreRateLimit;
using CustomerRegisterationFlow.LoggerService;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Presentation.ActionFilters;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NLog;
using CustomerRegisterationFlow.Infrastructure;
using CustomerRegisterationFlow.Persistence;
using CustomerRegisterationFlow.Application;
using AutoWrapper;
using CustomerRegisterationFlow.Application.Responses;

var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureLocalization();
// Add services to the container.
builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.ConfigureResponseCaching();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.ConfigureSwagger();
//builder.Services.ConfigureHttpCacheHeaders();
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() => new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson().Services.BuildServiceProvider().GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>().First();
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
}).AddXmlDataContractSerializerFormatters().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.ConfigureApplicationService();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.ConfigureLoggerServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();


var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseApiResponseAndExceptionWrapper<MapResponse>(new AutoWrapperOptions { UseCustomSchema = true, ShowStatusCode = true });
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
app.UseResponseCaching();

//app.UseHttpCacheHeaders();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v2/swagger.json", "AE For Life API v2");
});
app.MapControllers();
app.Run();
