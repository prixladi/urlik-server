﻿using Microsoft.Extensions.DependencyInjection;
using Shamyr.Extensions.DependencyInjection;

namespace Shamyr.Urlik.Service.IoC
{
  internal static class ServiceAssembly
  {
    public static void AddServiceAssembly(this IServiceCollection services)
    {
      services.AddDefaultConventions<Startup>();
    }
  }
}
