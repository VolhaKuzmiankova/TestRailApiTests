using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TestRail.Models;

namespace TestRail.Assertion
{
    public static class ProjectAssertion
    {
        private static IConfigurationRoot _config;

        public static void AssertProject(ProjectResponse expected, ProjectResponse actual)
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            actual.Id.Should().Be(actual.Id);
            actual.Name.Should().Be(expected.Name);
            actual.Announcement.Should().Be(expected.Announcement);
            actual.ShowAnnouncement.Should().Be(expected.ShowAnnouncement);
            actual.IsCompleted.Should().Be(expected.IsCompleted);
            actual.CompletedOn.Should().Be(null);
            actual.SuiteMode.Should().Be(3);
            actual.Url.Should().BeEquivalentTo($"{_config["AppUrl"]}index.php?/projects/overview/{actual.Id}");
        }
    }
}