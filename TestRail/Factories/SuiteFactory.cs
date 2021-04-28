using Bogus;
using TestRail.Models;

namespace TestRail.Factories
{
    public static class SuiteFactory
    {
        public static CreateSuiteModel GetSuiteModel()
        {
            return new Faker<CreateSuiteModel>()
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence(5))
                .Generate();
        }
    }
}