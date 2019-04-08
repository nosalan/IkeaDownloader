using System.Collections.Generic;
using System.Threading.Tasks;

namespace IkeaDownloader.Ports
{
  public interface IGetHandler
  {
    Task<string> Handle();
  }
}