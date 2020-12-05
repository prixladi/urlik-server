using Microsoft.Extensions.DependencyInjection;
using Shamyr.Extensions.DependencyInjection;
using Shamyr.Urlik.Service.HostedServices;

namespace Shamyr.Urlik.Service.IoC
{
  internal static class ServiceAssembly
  {
    public static void AddServiceAssembly(this IServiceCollection services)
    {
      services.AddHostedService<RedisInitHostedService>();
      services.AddHostedService<RedisTrimService>();

      services.AddDefaultConventions<Startup>();
    }
  }
}
