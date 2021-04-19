using System.Collections.Generic;
using TestRail.Constants;

namespace TestRail.Mocks
{
    public class SuiteMocks
    {
        public static IEnumerable<object[]> IncorrectValues()
        {
            var incorrectId = 3;
            var message = ErrorMessageConstants.IncorrectSuiteIdMessage;

            return new List<object[]>()
            {
                new object[]
                {
                    incorrectId,
                    message
                }
            };
        }
    }
}