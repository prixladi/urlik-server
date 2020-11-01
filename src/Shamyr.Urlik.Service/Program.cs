using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Shamyr.Urlik.Service;

await new WebHostBuilder()
  .UseKestrel()
  .UseStartup<Startup>()
  .Build()
  .RunAsync();
