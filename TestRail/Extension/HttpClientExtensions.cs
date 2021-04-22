using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestRail.Extension
{
    public static class HttpClientExtensions
    {
        public static async Task<T> Deserialize<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
        public static async Task<T> GetContentModel<T>(this HttpResponseMessage response)
        {
            return await response.Content.Deserialize<T>();
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var content = new StringContent
            (
                JsonConvert.SerializeObject(data),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
            );
            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string url)
        {
            return httpClient.PostAsJsonAsync(url, string.Empty);
        }
    }
}