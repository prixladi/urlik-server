using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shamyr.Cloud.Identity.Client.Authentication;
using Shamyr.Urlik.Service.Configs;
using Shamyr.Urlik.Service.Factories;

namespace Shamyr.Urlik.Service
{
  public sealed class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLogging(LoggingConfig.Setup);
      services.AddCors();

      services.AddControllers();
      services.AddExceptionHandling();

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = IdentityAuthenticationDefaults._AuthenticationScheme;
        options.DefaultChallengeScheme = IdentityAuthenticationDefaults._AuthenticationScheme;
      })
      .AddAuthorityBearerAuthentication<AuthorityClientConfig, PrincipalFactory>();

      services.AddMediatR(typeof(Startup));

      services.AddApplicationInsightsTelemetry(AppInsightsConfig.Setup);
      services.AddApplicationInsightsLogger(RoleNames._UrlikService);

      services.AddDatabaseContext<DatabaseConfig>();
      services.AddSwaggerGen(SwaggerConfig.SetupSwaggerGen);
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseCors(CorsConfig.Setup);
      app.UseExceptionHandling();
      app.UseRouting();
      app.UseAuthorization();

      app.UseSwagger(SwaggerConfig.SetupSwagger);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
