﻿using Microsoft.ApplicationInsights.AspNetCore.Extensions;

namespace Shamyr.Urlik.Service.Configs
{
  public static class AppInsightsConfig
  {
    public static void Setup(ApplicationInsightsServiceOptions options)
    {
      options.InstrumentationKey = EnvVariable.TryGet(EnvVariables._AppInsightsKey);
      options.EnableAdaptiveSampling = false;
      options.EnableDebugLogger = false;
    }
  }
}
