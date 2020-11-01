using System;

namespace Shamyr.Urlik.Service.Configs
{
  public class RedisConfig: IRedisConfig
  {
    public string RedisConfiguration => EnvVariable.Get(EnvVariables._RedisUrl);
  }
}
