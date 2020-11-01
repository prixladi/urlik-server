using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Shamyr.Cloud.Swashbuckle;

namespace Shamyr.Urlik.Service.Swagger
{
  public class AuthFilter: AspNetCore.Swashbuckle.Filters.AuthorizedEndpointsOperationFilterBase
  {
    protected override OpenApiSecurityRequirement? GetSecurityRequirement()
    {
      var scheme = new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = SwaggerGenOptionsExtensions._SecurityDefinitionName
        }
      };

      return new OpenApiSecurityRequirement
      {
        [scheme] = Array.Empty<string>()
      };
    }

    protected override IEnumerable<(int statusCode, string description)> GetStatuses()
    {
      yield return (StatusCodes.Status401Unauthorized, "User is not authorized.");
    }
  }
}
