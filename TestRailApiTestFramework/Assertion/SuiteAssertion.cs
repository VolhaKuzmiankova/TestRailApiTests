using FluentAssertions;
using TestRail.Models;
using static TestRail.Settings.AppSettings;

namespace TestRail.Assertion
{
    public static class SuiteAssertion
    {
        public static void AssertSuite(SuiteResponse expected, SuiteResponse actual)
        {
            actual.Id.Should().BePositive();
            actual.Name.Should().Be(expected.Name);
            actual.IsCompleted.Should().Be(expected.IsCompleted);
            actual.Description.Should().Be(expected.Description);
            actual.ProjectId.Should().Be(actual.ProjectId);
            actual.IsBaseLine.Should().BeFalse();
            actual.IsMaster.Should().BeFalse();
            actual.CompletedOn.Should().BeNull();
            actual.Url.Should()
                .BeEquivalentTo($"{Configuration["Services:TestRailApp:AppUrl"]}index.php?/suites/view/{actual.Id}");
        }
    }
}