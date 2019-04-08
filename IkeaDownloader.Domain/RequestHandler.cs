using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functional.Maybe;
using IkeaDownloader.Ports;

namespace IkeaDownloader.Domain
{
  class RequestHandler : IGetHandler
  {
    private readonly IProductWebpageDownloader _productWebpageDownloader;
    private readonly IProductPageParser _productPageParser;
    private readonly IDb _db;

    public RequestHandler(
      IProductWebpageDownloader productWebpageDownloader,
      IProductPageParser productPageParser,
      IDb db)
    {
      _productWebpageDownloader = productWebpageDownloader;
      _productPageParser = productPageParser;
      _db = db;
    }

    public async Task<string> Handle()
    {
      var productId = "40341144";
      var pageHtml = await _productWebpageDownloader.GetPageHtml(productId);
      var priceFromPage = _productPageParser.GetPrice(pageHtml);
      var priceFromDb = await _db.GetPrice(productId);

      if (priceFromPage.HasValue)
      {
        await _db.SetPrice(productId, priceFromPage.Value);
      }

      return FormatOutput(priceFromPage, priceFromDb, productId);
    }

    private static string FormatOutput(Maybe<decimal> priceFromPage, Maybe<decimal> priceFromDb, string productId)
    {
      if (priceFromPage.IsNothing() || priceFromDb.IsNothing())
        return
          $"[Product {productId}] NEW Price: {priceFromPage.ReturnToString("<NONE>")} OLD Price: {priceFromDb.ReturnToString("<NONE>")}";

      if (priceFromDb.Value == priceFromPage.Value)
        return $"[Product {productId}] Price ({priceFromPage}) is the SAME";

      if (priceFromDb.Value < priceFromPage.Value)
        return $"[Product {productId}] Price now ({priceFromPage}) is HIGHER than in the past ({priceFromDb})";

      return $"[Product {productId}] Price now ({priceFromPage}) is LOWER than in the past ({priceFromDb})";
    }
  }
}