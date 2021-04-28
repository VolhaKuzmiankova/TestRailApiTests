using System.Net;
using System.Threading.Tasks;
using Allure.Xunit.Attributes;
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
    public class GetProjectTests : BaseTest
    {
        public GetProjectTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [AllureXunit(DisplayName = "GET index.php?/api/v2/get_project/{projectId} when project is existed returns 200")]
        public async Task GetProject_WhenProjectIsExisted_ShouldReturnOk()
        {
            //Arrange
            SetUpAuthorization();
            var createdProject = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());

            //Act
            var response = await _projectService.GetProject(createdProject.Id);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.OK, "Expected OK status.");
            var responseProject = await response.GetContentModel<ProjectResponse>();
            ProjectAssertion.AssertProject(createdProject, responseProject);
        }

        [AllureXunitTheory(DisplayName = "GET index.php?/api/v2/get_project{projectId} when projectId has incorrect value returns 400")]
        [MemberData(nameof(ProjectMocks.IncorrectProjectId), MemberType = typeof(ProjectMocks))]
        public async Task GetProject_WhenProjectIdHasIncorrectValue_ShouldReturnBadRequest(string id, string message)
        {
            //Arrange
            SetUpAuthorization();

            //Act
            var response = await _projectService.GetProject(id);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
            var error = await response.GetErrors();
            ErrorAssertion.ErrorAssert(error, message);
        }

        [AllureXunit(DisplayName = "GET index.php?/api/v2/get_project/{projectId} when user is unauthorized returns 401")]
        public async Task GetProject_WhenUnauthorized_ShouldReturnUnauthorized()
        {
            //Arrange
            SetUpAuthorization();
            var projectModel = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            ClearAuthorization();

            //Act
            var response = await _projectService.GetProject(projectModel.Id);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.Unauthorized, "Expected Unauthorized status.");
            var error = await response.GetErrors();
            ErrorAssertion.ErrorAssert(error, ErrorMessageConstants.AuthenticationFailedMessage);
        }
    }
}