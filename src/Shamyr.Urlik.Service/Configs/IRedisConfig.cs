using System;

namespace Shamyr.Urlik.Service.Configs
{
  public interface IRedisConfig
  {
    public string Host { get; }
    public int Port { get; }
    int HitsCapacity { get; }
    TimeSpan HitsTrimInterval { get; }
  }
}
