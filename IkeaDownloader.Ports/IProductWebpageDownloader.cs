using System.Threading.Tasks;

namespace IkeaDownloader.Ports
{
  public interface IProductWebpageDownloader
  {
    Task<string> GetPageHtml(string productId);
  }
}