using System.Data.Common;
using Functional.Maybe;
using IkeaDownloader.Domain;
using IkeaDownloader.Ports;
using NSubstitute;
using NUnit.Framework;

namespace IkeaDownloader.ComponentTests
{
  public class ComponentDriver
  {
    private string _requestResult;
    private readonly IProductWebpageDownloader _productWebpageDownloader;
    private readonly IDb _database;
    private string _productId = "40341144";

    private ComponentDriver()
    {
      _productWebpageDownloader = Substitute.For<IProductWebpageDownloader>();
      _database = new FakeDatabase();

      var domainLogicRoot = new DomainLogicRoot(_productWebpageDownloader, _database);
      GetHandler = domainLogicRoot.GetHandler;
    }

    public IGetHandler GetHandler { get; set; }

    public void RequestIsSentToMyService()
    {
      _requestResult = GetHandler.Handle().Result;
    }

    public void PageDownloaderReturnsPageWithPrice(string price)
    {
      _productWebpageDownloader.GetPageHtml(_productId).Returns($@"<html><body><span id=""price5"">{price}</span></body></html>");
    }

    public void PageDownloaderReturnsNothing()
    {
      _productWebpageDownloader.GetPageHtml(_productId).Returns($@"<html><body></body></html>");
    }
    
    public void ShouldRespondWithText(string expectedText)
    {
      Assert.AreEqual(expectedText, _requestResult);
    }

    public static ComponentDriver Create()
    {
      return new ComponentDriver();
    }

    public void DatabaseHasPrice(string productId, decimal price)
    {
      _database.SetPrice(productId, price);
    }
  }
}