using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Functional.Maybe;
using IkeaDownloader.Ports;
using StackExchange.Redis;
using static System.String;

namespace IkeaDownloader
{
  public class RedisDb : IDb
  {
    readonly Lazy<ConnectionMultiplexer> _redis
      = new Lazy<ConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

    public async Task<Maybe<decimal>> GetPrice(string productId)
    {
      var result = await _redis.Value.GetDatabase().HashGetAsync(productId, "current");
      return result.HasValue ? Convert.ToDecimal(result.ToString()).ToMaybe() : Maybe<decimal>.Nothing;
    }

    public async Task SetPrice(string productId, decimal price)
    {
      await SetHashField(productId, "current", price.ToString("F"));
    }

    private async Task SetHashField(string hashId, string field, string value)
    {
      await _redis.Value.GetDatabase().HashSetAsync(hashId, field, value);
    }
  }
}