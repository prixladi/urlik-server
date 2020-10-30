using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Shamyr.Urlik.Service.Configs
{
  public static class CorsConfig
  {
    public static void Setup(CorsPolicyBuilder builder)
    {
      builder.AllowAnyHeader();
      builder.AllowAnyMethod();
      builder.AllowAnyOrigin();
    }
  }
}
