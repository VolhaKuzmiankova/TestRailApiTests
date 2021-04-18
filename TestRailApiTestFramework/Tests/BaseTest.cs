using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TestRail.Client;
using TestRail.Services;
using TestRail.Steps;
using Xunit.Abstractions;

namespace TestRail.Tests
{
    public abstract class BaseTest
    {
        private readonly IConfigurationRoot _config;
        private readonly HttpClient _client;
        protected readonly ProjectService _projectService;
        protected readonly SuiteService _suiteService;
        protected readonly ProjectSteps _projectSteps;
        protected readonly SuiteSteps _suiteSteps;

        protected BaseTest(ITestOutputHelper testOutputHelper)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders()
                    .AddXUnit(testOutputHelper);
            });

            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            _client = new HttpClient(new LoggingHandler(new HttpClientHandler(), loggerFactory))
            {
                BaseAddress = new Uri(_config["AppUrl"])
            };

            _projectService = new ProjectService(_client);
            _projectSteps = new ProjectSteps(_projectService);
            _suiteService = new SuiteService(_client);
            _suiteSteps = new SuiteSteps(_suiteService);
        }

        protected void SetUpAuthorization()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{_config["Username"]}:{_config["Password"]}")
                )
            );
        }

        protected void ClearAuthorization()
        {
            _client.DefaultRequestHeaders.Clear();
        }
    }
}