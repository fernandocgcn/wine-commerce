using System.Threading.Tasks;

namespace WCDomain.Services
{
    public interface IApiReaderService
    {
        public Task<T> ReadUriAsync<T>(string URI);
    }
}
