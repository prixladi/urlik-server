using Microsoft.AspNetCore.Mvc;
using Shamyr.AspNetCore.Attributes;
using Shamyr.Cloud.Bson;

namespace Shamyr.Urlik.Service.Configs
{
  public class MvcConfig
  {
    public static void SetupMvcOptions(MvcOptions options)
    {
      options.Filters.Add<ApiValidationAttribute>();
      options.ModelBinderProviders.Insert(0, new ObjectIdBinderProvider());
    }

    public static void SetupJsonOptions(JsonOptions options)
    {
      options.JsonSerializerOptions.Converters.Add(new ObjectIdJsonConverter());
    }
  }
}
