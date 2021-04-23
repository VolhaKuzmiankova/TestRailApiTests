using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using TestRail.Client;
using TestRail.Extension;
using TestRail.Services;
using TestRail.Steps;
using Xunit.Abstractions;

namespace TestRail.Tests
{
    public abstract class BaseTest
    {
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

            _client = new HttpClient(new LoggingHandler(new HttpClientHandler(), loggerFactory))
            {
                BaseAddress = new Uri(Startup.AppSettings.Services.TestRailApp.AppUrl)
            };

            _projectService = new ProjectService(_client);
            _projectSteps = new ProjectSteps(_projectService);
            _suiteService = new SuiteService(_client);
            _suiteSteps = new SuiteSteps(_suiteService);
            
        }

        protected void SetUpAuthorization()
        {
            var data = $"{Startup.AppSettings.Users.UserName}:{Startup.AppSettings.Users.Password}";
            _client.SetUpAuthorization(data);
        }

        protected void ClearAuthorization()
        {
            _client.ClearAuthorization();
        }
    }
}