using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;
using TestRail.Client;
using TestRail.Services;
using TestRail.Steps;
using Xunit.Abstractions;
using static TestRail.Settings.AppSettings;

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
                BaseAddress = new Uri(Configuration["Services:TestRailApp:AppUrl"])
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
                    Encoding.UTF8.GetBytes($"{Configuration["Users:UserName"]}:{Configuration["Users:Password"]}")
                )
            );
        }

        protected void ClearAuthorization()
        {
            _client.DefaultRequestHeaders.Clear();
        }
    }
}