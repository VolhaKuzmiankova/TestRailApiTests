using Bogus;
using TestRail.Models;

namespace TestRail.Factories
{
    public static class SuiteFactory
    {
        public static CreateSuiteModel GetSuiteModel()
        {
            return new CreateSuiteModel
            {
                Name = new Faker().Name.Random.Word(),
                Description = new Faker().Lorem.Sentence(5)
            };
        }
    }
}