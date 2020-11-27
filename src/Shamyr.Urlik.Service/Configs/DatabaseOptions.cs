using System;
using System.Collections.Generic;
using System.Reflection;
using Shamyr.Extensions.MongoDB;
using Shamyr.Urlik.Database;

namespace Shamyr.Urlik.Service.Configs
{
  public class DatabaseOptions: IDatabaseOptions
  {
    public List<Assembly> MigrationAssemblies => new List<Assembly> { typeof(EnvVariables).Assembly };
    public List<Assembly> DatabaseAssemblies => new List<Assembly> { typeof(DbCollections).Assembly };
    public int MetadataVersion => 2;
    public TimeSpan LockDuration => TimeSpan.FromMinutes(1);

    public bool MapDiscriminators => true;
    public bool UseCammelCaseConvention => true;
    public bool IgnoreFieldsIfNull => true;
  }
}
