using NUnit.Framework;
using TddXt.AnyRoot.Numbers;
using static TddXt.AnyRoot.Root;

namespace IkeaDownloader.ComponentTests
{
  [TestFixture]
  class ComponentTests
  {
    [Test]
    public void ShouldInformThatNewPriceIsLowerThanOld()
    {
      // GIVEN
      var newPrice = "1900";
      var oldPrice = 2000m;
      var driver = ComponentDriver.Create();
      driver.PageDownloaderReturnsPageWithPrice(newPrice);
      driver.DatabaseHasPrice(ProductId, oldPrice);

      // WHEN
      driver.RequestIsSentToMyService();

      // THEN
      driver.ShouldRespondWithText($"[Product {ProductId}] Price now ({newPrice}) is LOWER than in the past ({oldPrice})");
    }

    [Test]
    public void ShouldInformThatNewPriceIsHigherThanOld()
    {
      // GIVEN
      var newPrice = "2000";
      var oldPrice = 1900m;
      var driver = ComponentDriver.Create();
      driver.PageDownloaderReturnsPageWithPrice(newPrice);
      driver.DatabaseHasPrice(ProductId, oldPrice);

      // WHEN
      driver.RequestIsSentToMyService();

      // THEN
      driver.ShouldRespondWithText($"[Product {ProductId}] Price now ({newPrice}) is HIGHER than in the past ({oldPrice})");
    }

    [Test]
    public void ShouldReturnNoneForBothPricesIfNoPriceInWebsiteAndDatabase()
    {
      // GIVEN
      var driver = ComponentDriver.Create();
      driver.PageDownloaderReturnsNothing();

      // WHEN
      driver.RequestIsSentToMyService();

      // THEN
      driver.ShouldRespondWithText($"[Product {ProductId}] NEW Price: <NONE> OLD Price: <NONE>");
    }

    [Test]
    public void ShouldReturnNoneForNewPriceIfTheValueIsOnlyInDatabase()
    {
      // GIVEN
      var driver = ComponentDriver.Create();
      var newPrice = Any.Decimal();
      driver.PageDownloaderReturnsPageWithPrice(newPrice.ToString("##,#"));

      // WHEN
      driver.RequestIsSentToMyService();

      // THEN
      driver.ShouldRespondWithText($"[Product {ProductId}] NEW Price: {newPrice} OLD Price: <NONE>");
    }

    [Test]
    public void ShouldReturnNoneForOldPriceIfTheValueIsOnlyOnWebsite()
    {
      // GIVEN
      var driver = ComponentDriver.Create();
      var oldPrice = Any.Decimal();
      driver.DatabaseHasPrice("40341144", oldPrice);

      // WHEN
      driver.RequestIsSentToMyService();

      // THEN
      driver.ShouldRespondWithText($"[Product {ProductId}] NEW Price: <NONE> OLD Price: {oldPrice}");
    }

    private const string ProductId = "40341144";
  }
}