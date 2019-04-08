using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Functional.Maybe;

namespace IkeaDownloader.Ports
{
  public interface IDb
  {
    Task<Maybe<decimal>> GetPrice(string productId);
    Task SetPrice(string productId, decimal price);
  }
}
