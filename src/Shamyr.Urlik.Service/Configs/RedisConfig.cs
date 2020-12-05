using System;

namespace Shamyr.Urlik.Service.Configs
{
  public class RedisConfig: IRedisConfig
  {
    public string Host => EnvVariable.Get(EnvVariables._RedisHost);
    public int Port => int.Parse(EnvVariable.Get(EnvVariables._RedisPort));

    public int HitsCapacity
    {
      get
      {
        var configCapacity = EnvVariable.TryGet(EnvVariables._RedisHitsCapacity);
        if (configCapacity is null)
          return 200; // default

        return int.Parse(configCapacity);
      }
    }

    public TimeSpan HitsTrimInterval
    {
      get
      {
        var configSeconds = EnvVariable.TryGet(EnvVariables._RedisHitsTrimInterval);
        if (configSeconds is null)
          return TimeSpan.FromSeconds(20); // default

        var seconds = int.Parse(configSeconds);
        return TimeSpan.FromSeconds(seconds);
      }
    }
  }
}
