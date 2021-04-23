using FluentAssertions;
using TestRail.Models;

namespace TestRail.Assertion
{
    public static class ErrorAssertion
    {
        public static void ErrorAssert(Error error, string messageOfError)
        {
            error.Message.Should().Be(messageOfError);
        }
    }
}