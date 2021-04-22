using FluentAssertions;
using TestRail.Constants;
using TestRail.Models;

namespace TestRail.Assertion
{
    public static class ProjectAssertion
    {
        public static void AssertProject(ProjectResponse expected, ProjectResponse actual)
        {
            actual.Id.Should().Be(actual.Id);
            actual.Name.Should().Be(expected.Name);
            actual.Announcement.Should().Be(expected.Announcement);
            actual.ShowAnnouncement.Should().Be(expected.ShowAnnouncement);
            actual.IsCompleted.Should().Be(expected.IsCompleted);
            actual.CompletedOn.Should().BeNull();
            actual.SuiteMode.Should().Be(SuiteModeConstants.SuiteModeValue);
            actual.Url.Should()
                .BeEquivalentTo(
                    $"{Startup.AppSettings.Services.TestRailApp.AppUrl}index.php?/projects/overview/{actual.Id}");
        }
    }
}