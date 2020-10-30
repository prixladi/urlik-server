using Microsoft.Extensions.Logging;

namespace Shamyr.Urlik.Service.Configs
{
  public static class LoggingConfig
  {
    public static void Setup(ILoggingBuilder builder)
    {
      builder.AddConsole();
    }
  }
}
