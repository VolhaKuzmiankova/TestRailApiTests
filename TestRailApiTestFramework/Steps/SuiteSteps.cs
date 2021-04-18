using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TestRail.Extension;
using TestRail.Models;
using TestRail.Services;

namespace TestRail.Steps
{
    public class SuiteSteps
    {
        private readonly SuiteService _suiteService;

        public SuiteSteps(SuiteService suiteService)
        {
            _suiteService = suiteService;
        }

        public async Task<CreateSuiteModel> AddSuite(int projectId, CreateSuiteModel createCreateSuiteModel)
        {
            var response = await _suiteService.AddSuite(projectId, createCreateSuiteModel);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            return await response.GetContentModel<CreateSuiteModel>();
        }
    }
}