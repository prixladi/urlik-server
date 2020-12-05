using System;
using System.Linq;
using Shamyr.Extensions.DependencyInjection;

namespace Shamyr.Urlik.Service.Services
{
  [Singleton]
  public class RandomService: IRandomService
  {
    private readonly Random fRandom;

    public RandomService()
    {
      fRandom = new Random();
    }

    public string GeneratePath(int length)
    {
      const string pool = "abcdefghijklmnopqrstuvwxyzABVDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      var chars = Enumerable.Range(0, length).Select(x => pool[fRandom.Next(0, pool.Length)]);
      return new string(chars.ToArray());
    }
  }
}
