using System.Collections.Generic;

namespace TestRail.Mocks
{
    public class SuiteMocks
    {
        public static IEnumerable<object[]> IncorrectValues()
        {
            var incorrectId = 3;
            var message = "Field :suite_id is not a valid test suite.";

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