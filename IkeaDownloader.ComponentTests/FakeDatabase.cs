using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Functional.Maybe;
using IkeaDownloader.Ports;

namespace IkeaDownloader.ComponentTests
{
  class FakeDatabase : IDb
  {
    private readonly Dictionary<string, decimal> _entries = new Dictionary<string, decimal>();

    public Task<Maybe<decimal>> GetPrice(string productId)
    {
      if (_entries.TryGetValue(productId, out var result))
      {
        return Task.FromResult(result.ToMaybe());
      }

      return Task.FromResult(Maybe<decimal>.Nothing);
    }

    public Task SetPrice(string productId, decimal price)
    {
      _entries[productId] = price;
      return Task.CompletedTask;
    }
  }
}
