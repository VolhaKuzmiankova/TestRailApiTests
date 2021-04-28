using TestRail.Models;

namespace TestRail.Factories
{
    public static class SuiteResponseFactory
    {
        public static SuiteResponse SuiteResponseModel(CreateSuiteModel createSuiteModel)
        {
            return new SuiteResponse()
            {
                Name = createSuiteModel.Name,
                Description = createSuiteModel.Description,
                Id = createSuiteModel.Id
            };
        }
    }
}