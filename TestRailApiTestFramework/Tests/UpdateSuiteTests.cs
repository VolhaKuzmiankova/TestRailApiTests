using System.Net;
using System.Threading.Tasks;
using Allure.Xunit.Attributes;
using FluentAssertions;
using TestRail.Assertion;
using TestRail.Extension;
using TestRail.Factories;
using TestRail.Mocks;
using TestRail.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestRail.Tests
{
    public class UpdateSuiteTests : BaseTest
    {
        public UpdateSuiteTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/update_suite/{suiteId} when suite is existed returns 200")]
        public async Task UpdateSuite_WhenSuiteIsExistedShouldReturnOk()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var createdSuite = await _suiteSteps.AddSuite(project.Id, SuiteFactory.GetSuiteModel());

            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.UpdateSuite(createdSuite.Id, suiteModel);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.OK, "Expected OK status.");
            var responseSuite = await response.GetContentModel<SuiteResponse>();
            var expectedSuite = ResponseFactory.SuiteResponseModel(suiteModel);
            SuiteAssertion.AssertSuite(expectedSuite, responseSuite);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/update_suite/{suiteId} when user is unauthorized returns 401")]
        public async Task UpdateSuite_WhenNotAuthorized_ShouldReturnUnauthorized()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var createdSuite = await _suiteSteps.AddSuite(project.Id, SuiteFactory.GetSuiteModel());
            ClearAuthorization();
            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.UpdateSuite(createdSuite.Id, suiteModel);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.Unauthorized, "Expected Unauthorized status.");
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be("Authentication failed: invalid or missing user/password or session cookie.");
        }

        [AllureXunitTheory(DisplayName = "POST index.php?/api/v2/update_suite/{suiteId} when suiteId has incorrect value returns 400")]
        [MemberData(nameof(SuiteMocks.IncorrectValues), MemberType = typeof(SuiteMocks))]
        public async Task UpdateSuite_WhenSuiteIdIsIncorrect_ShouldReturnBadRequest(string id, string message)
        {
            //Arrange
            SetUpAuthorization();
            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.UpdateSuite(id, suiteModel);
            //Assert
            response.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(message);
        }
    }
}