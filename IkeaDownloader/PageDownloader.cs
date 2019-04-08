using System.Net.Http;
using System.Threading.Tasks;
using IkeaDownloader.Ports;

namespace IkeaDownloader
{
  public class ProductSiteDownloader : IProductWebpageDownloader
  {
    public async Task<string> GetPageHtml(string productId)
    {
      var hc = new HttpClient();
      hc.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.186 Safari/535.1");
      var result = await hc.GetAsync($"https://www.ikea.com/pl/pl/catalog/products/{productId}/");
      return await result.Content.ReadAsStringAsync();
    }
  }
}
