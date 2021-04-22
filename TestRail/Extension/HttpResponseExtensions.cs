using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestRail.Exceptions;
using TestRail.Models;

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

        public static Task<Error> GetErrors(this HttpResponseMessage responseMessage)
        {
            return responseMessage.GetContentModel<Error>();
        }
    }
}