using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TestRail.Client
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public LoggingHandler(HttpMessageHandler innerHandler, ILoggerFactory loggerFactory) : base(innerHandler)
        {
            _logger = loggerFactory.CreateLogger<LoggingHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.ToString());
            if (request.Content != null)
            {
                _logger.LogInformation(await request.Content.ReadAsStringAsync());
            }

            var response = await base.SendAsync(request, cancellationToken);

            _logger.LogInformation(response.ToString());
            if (response.Content != null)
            {
                _logger.LogInformation(await response.Content.ReadAsStringAsync());
            }

            return response;
        }
    }
}