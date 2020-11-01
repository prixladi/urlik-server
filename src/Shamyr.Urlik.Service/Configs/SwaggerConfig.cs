using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Shamyr.Cloud.Swashbuckle;
using Shamyr.Cloud.Swashbuckle.Bson;
using Shamyr.Urlik.Service.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shamyr.Urlik.Service.Configs
{
  public static class SwaggerConfig
  {
    private const string _V1Route = "v1";
    private const string _V1Title = "Shamyr Urlik Service";

    public static void SetupSwagger(SwaggerOptions options)
    {
      options.RouteTemplate = "docs/{documentName}/swagger.json";
    }

    public static void SetupSwaggerGen(SwaggerGenOptions options)
    {
      options.OperationFilter<AuthFilter>();
      options.OperationFilter<FlattenObjectIdOperationFilter>();

      options.AddIndentitySecurity();

      options.SwaggerDoc(_V1Route, new OpenApiInfo { Title = _V1Title, Version = _V1Route });
      options.DocInclusionPredicate((version, apiDescription) => apiDescription.RelativePath.Contains($"/{version}/"));

      foreach (string xmlFile in Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml", SearchOption.AllDirectories))
        options.IncludeXmlComments(xmlFile);
    }
  }
}
