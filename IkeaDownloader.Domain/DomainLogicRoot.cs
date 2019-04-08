using System;
using System.Collections.Generic;
using System.Text;
using IkeaDownloader.Ports;

namespace IkeaDownloader.Domain
{
  public class DomainLogicRoot
  {
    public IGetHandler GetHandler { get; }

    public DomainLogicRoot(IProductWebpageDownloader productWebpageDownloader, IDb database)
    {
      var productPageParser = new ProductPageParser();
      GetHandler = new RequestHandler(productWebpageDownloader, productPageParser, database);
    }
  }
}
