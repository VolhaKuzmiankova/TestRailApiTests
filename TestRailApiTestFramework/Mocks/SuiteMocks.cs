using System.Collections.Generic;
using Bogus;
using TestRail.Constants;

namespace TestRail.Mocks
{
    public class SuiteMocks
    {
        public static IEnumerable<object[]> IncorrectValues()
        {
            var incorrectSuiteId = new Faker().Random.Number(0);

            var incorrectId = new Faker().Random.Word();

            return new List<object[]>()
            {
                new object[]
                {
                    incorrectId,
                    ErrorMessageConstants.NotAValidSuiteMessage
                },
                new object[]
                {
                    incorrectSuiteId,
                    ErrorMessageConstants.IncorrectSuiteIdMessage
                }
            };
        }
    }
}