using FluentAssertions;
using TestRail.Models;

namespace TestRail.Assertion
{
    public static class ProjectAssertion
    {
        public static void AssertProject(ProjectResponse expected, ProjectResponse actual)
        {
            actual.Id.Should().NotBe(null);
            actual.Name.Should().Be(expected.Name);
            actual.Announcement.Should().Be(expected.Announcement);
            actual.ShowAnnouncement.Should().Be(expected.ShowAnnouncement);
            actual.IsCompleted.Should().Be(expected.IsCompleted);
            actual.Url.Should().EndWith(actual.Id.ToString());
        }
    }
}