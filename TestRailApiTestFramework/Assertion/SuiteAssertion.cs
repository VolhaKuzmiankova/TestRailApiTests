using FluentAssertions;
using TestRail.Models;

namespace TestRail.Assertion
{
    public static class SuiteAssertion
    {
        public static void AssertSuite(SuiteResponse expected, SuiteResponse actual)
        {
            actual.Id.Should().NotBe(null);
            actual.Name.Should().Be(expected.Name);
            actual.IsCompleted.Should().Be(expected.IsCompleted);
            actual.Url.Should().StartWith(actual.Url);
            actual.Url.Should().EndWith(actual.Id.ToString());
            actual.Description.Should().NotBe(null);
            actual.ProjectId.Should().NotBe(null);
        }
    }
}