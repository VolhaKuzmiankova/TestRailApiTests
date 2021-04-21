using System.Net;
using System.Threading.Tasks;
using Allure.Xunit.Attributes;
using FluentAssertions;
using TestRail.Assertion;
using TestRail.Constants;
using TestRail.Extension;
using TestRail.Factories;
using TestRail.Mocks;
using TestRail.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestRail.Tests
{
    public class AddSuiteTests : BaseTest
    {
        public AddSuiteTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [AllureXunit(DisplayName =
            "POST index.php?/api/v2/add_suite/{projectId} when fields have correct values returns 200")]
        public async Task AddSuite_WhenFieldsHaveCorrectValues_ShouldReturnOK_ShouldReturnOk()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.AddSuite(project.Id, suiteModel);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.OK, "Expected OK status.");
            var responseModel = await response.GetContentModel<SuiteResponse>();
            var expectedResponse = ResponseFactory.SuiteResponseModel(suiteModel);
            SuiteAssertion.AssertSuite(expectedResponse, responseModel);
        }

        [AllureXunit(DisplayName =
            "POST index.php?/api/v2/add_suite/{projectId} when user is unauthorized returns 401")]
        public async Task CreateProject_WhenUnAuthorized_ShouldReturnUnauthorized()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var suiteModel = SuiteFactory.GetSuiteModel();
            ClearAuthorization();

            //Act
            var response = await _suiteService.AddSuite(project.Id, suiteModel);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.Unauthorized, "Expected Unauthorized status.");
            var error = await response.GetContentModel<Error>();
            error.Message.Should()
                .Be(ErrorMessageConstants.AuthenticationFailedMessage);
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/add_suite/{projectId} when projectId has incorrect value returns 400")]
        [MemberData(nameof(ProjectMocks.IncorrectProjectId), MemberType = typeof(ProjectMocks))]
        public async Task AddSuite_WhenProjectIdHasIncorrectValue_ShouldReturnBadRequest(string id, string message )
        {
            //Arrange
            SetUpAuthorization();
            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.AddSuite(id, suiteModel);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(message);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_suite/{projectId} when project is not exist")]
        public async Task AddSuite_WhenProjectIsNotExist_ShouldReturnBadRequest()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var suiteModel = SuiteFactory.GetSuiteModel();
            await _projectService.Delete(project.Id);

            //Act
            var response = await _suiteService.AddSuite(project.Id, suiteModel);
            
            //Assert
            response.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
        }
        
    }
}