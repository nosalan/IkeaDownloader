using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using Functional.Maybe;
using HtmlAgilityPack;

namespace IkeaDownloader.Domain
{
  internal interface IProductPageParser
  {
    Maybe<decimal> GetPrice(string pageHtml);
  }

  class ProductPageParser : IProductPageParser
  {
    public Maybe<decimal> GetPrice(string pageHtml)
    {
      var doc = new HtmlDocument();
      doc.LoadHtml(pageHtml);
      var priceString = GetPriceStringForSpanId(doc, "price5");
      if (string.IsNullOrEmpty(priceString))
        priceString = GetPriceStringForSpanId(doc, "price1");

      if (string.IsNullOrEmpty(priceString))
        return Maybe<decimal>.Nothing;

      return Convert.ToDecimal(priceString, new CultureInfo("PL-pl")).ToMaybe();
    }

    private static string GetPriceStringForSpanId(HtmlDocument doc, string spanId)
    {
      var priceString = doc.GetElementbyId(spanId)
        ?.InnerText
        ?.Where(c => char.IsDigit(c) || c == ',')
        .Aggregate(string.Empty, (s, c) => s + c);
      return priceString;
    }
  }
}
