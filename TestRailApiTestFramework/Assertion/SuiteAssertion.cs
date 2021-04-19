using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TestRail.Models;

namespace TestRail.Assertion
{
    public static class SuiteAssertion
    {
        private static IConfigurationRoot _config;

        public static void AssertSuite(SuiteResponse expected, SuiteResponse actual)
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            actual.Id.Should().NotBe(null);
            actual.Name.Should().Be(expected.Name);
            actual.IsCompleted.Should().Be(expected.IsCompleted);
            actual.Description.Should().Be(expected.Description);
            actual.ProjectId.Should().Be(actual.ProjectId);
            actual.IsBaseLine.Should().Be(false);
            actual.IsMaster.Should().Be(false);
            actual.CompletedOn.Should().Be(null);
            actual.Url.Should().BeEquivalentTo($"{_config["AppUrl"]}index.php?/suites/view/{actual.Id}");
        }
    }
}