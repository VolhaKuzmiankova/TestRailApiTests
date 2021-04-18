using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestRail.Extension
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }

        public static async Task<T> GetContentModel<T>(this HttpResponseMessage response)
        {
            return await response.Content.ReadAsJsonAsync<T>();
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var content = new StringContent
            (
                JsonConvert.SerializeObject(data),
                Encoding.UTF8,
                "application/json"
            );
            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string url)
        {
            var content = new StringContent
            (
                "",
                Encoding.UTF8,
                "application/json"
            );
            return httpClient.PostAsync(url, content);
        }
    }
}