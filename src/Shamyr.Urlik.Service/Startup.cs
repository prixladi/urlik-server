using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Authority.Client.Authentication;
using Shamyr.Urlik.Service.Configs;
using Shamyr.Urlik.Service.Factories;
using Shamyr.Urlik.Service.IoC;

namespace Shamyr.Urlik.Service
{
  public sealed class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLogging(LoggingConfig.Setup);
      services.AddCors();

      services.AddControllers(MvcConfig.SetupMvcOptions)
        .AddJsonOptions(MvcConfig.SetupJsonOptions);

      services.AddExceptionHandling();

      services.AddServiceAssembly();

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = AuthorityAuthenticationDefaults._AuthenticationScheme;
        options.DefaultChallengeScheme = AuthorityAuthenticationDefaults._AuthenticationScheme;
      })
      .AddAuthorityBearerAuthentication<AuthorityClientConfig, PrincipalFactory>();

      services.AddMediatR(typeof(Startup));

      services.AddApplicationInsightsTelemetry(AppInsightsConfig.Setup);
      services.AddApplicationInsightsLogger(RoleNames._UrlikService);

      services.AddDatabaseContext<DatabaseConfig, DatabaseOptions>();
      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseCors(CorsConfig.Setup);
      app.UseExceptionHandling();
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseSwagger(SwaggerConfig.SetupSwagger);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
