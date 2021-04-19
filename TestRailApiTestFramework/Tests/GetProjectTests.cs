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
            var responseProject = await _projectSteps.GetProject(createdProject.Id);

            //Assert
            ProjectAssertion.AssertProject(createdProject, responseProject);
        }

        [AllureXunitTheory(DisplayName =
            "GET index.php?/api/v2/get_project{projectId} when projectId has incorrect value returns 400")]
        [MemberData(nameof(ProjectMocks.ProjectIncorrectValues), MemberType = typeof(ProjectMocks))]
        public async Task GetProjectWhenFieldHasIncorrectValue_ShouldReturnBadRequest(int id, string message)
        {
            //Arrange
            SetUpAuthorization();

            //Act
            var response = await _projectService.GetProject(id);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(message);
        }

        [AllureXunit(DisplayName =
            "GET index.php?/api/v2/get_project/{projectId} when user is unauthorized returns 401")]
        public async Task GetProject_WhenUnauthorized_ShouldReturnUnauthorized()
        {
            //Arrange
            SetUpAuthorization();
            var projectModel = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            ClearAuthorization();

            //Act
            var response = await _projectService.GetProject(projectModel.Id);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(ErrorMessageConstants.AuthenticationFailedMessage);
        }
    }
}