using System.Net;
using System.Net.Http;
using TestRail.Exceptions;

namespace TestRail.Extension
{
    public static class HttpResponseExtensions
    {
        public static void ResponseStatusCode(
            this HttpResponseMessage response,
            HttpStatusCode expectedStatusCode, string message)
        {
            if (response.StatusCode == expectedStatusCode) return;
            throw new StepsExceptions(
                $"Invalid response status code. Expected to be {expectedStatusCode} but found {response.StatusCode}" +
                $"\n{message}");
        }
    }
}