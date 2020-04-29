using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WCDomain.Services
{
    public class ApiReaderService : IApiReaderService
    {
        public async Task<T> ReadUriAsync<T>(string URI)
        {
            using var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(URI);
            T result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStreamAsync();
                result = await JsonSerializer.DeserializeAsync<T>(json);
            }
            else
                throw new HttpRequestException(httpResponse.ReasonPhrase);
            return result;
        }
    }
}
