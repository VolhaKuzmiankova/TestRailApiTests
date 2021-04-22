using System.Net;
using System.Threading.Tasks;
using Allure.Xunit.Attributes;
using FluentAssertions;
using TestRail.Constants;
using TestRail.Extension;
using TestRail.Factories;
using TestRail.Mocks;
using Xunit;
using Xunit.Abstractions;

namespace TestRail.Tests
{
    public class DeleteProjectTests : BaseTest
    {
        public DeleteProjectTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }


        [AllureXunit(DisplayName = "POST index.php?/api/v2/delete_project{projectId} when projectId is correct returns 200")]
        public async Task DeleteProject_WhenProjectIdIsCorrect_ShouldReturnOk()
        {
            //Arrange
            SetUpAuthorization();
            var projectModel = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());

            //Act
            var response = await _projectService.Delete(projectModel.Id);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.OK, "Expected OK status.");
        }

        [AllureXunitTheory(DisplayName = "POST index.php?/api/v2/delete_project{projectId} when projectId has incorrect value returns 400")]
        [MemberData(nameof(ProjectMocks.IncorrectProjectId), MemberType = typeof(ProjectMocks))]
        public async Task DeleteProject_WhenProjectIdHasIncorrectValue_ShouldReturnBadRequest(string id, string message)
        {
            //Arrange
            SetUpAuthorization();

            //Act
            var response = await _projectService.Delete(id);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
            var error = await response.GetErrors();
            error.Message.Should().Be(message);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/delete_project{projectId} when user is unauthorized returns 401")]
        public async Task DeleteProject_WhenUnauthorized_ShouldReturnUnauthorized()
        {
            //Arrange
            SetUpAuthorization();
            var projectModel = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            ClearAuthorization();

            //Act
            var response = await _projectService.Delete(projectModel.Id);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.Unauthorized, "Expected Unauthorized status.");
            var error = await response.GetErrors();
            error.Message.Should().Be(ErrorMessageConstants.AuthenticationFailedMessage);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/delete_project/{projectId} when the project was deleted before returns 400 ")]
        public async Task DeleteProject_WhenProjectWasDeleted_ShouldReturnBadRequest()
        {
            //Arrange
            SetUpAuthorization();
            var projectModel = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            await _projectService.Delete(projectModel.Id);

            //Act
            var deleteResponse = await _projectService.Delete(projectModel.Id);
            var getResponse = await _projectService.GetProject(projectModel.Id);

            // //Assert
            deleteResponse.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status");
            getResponse.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
            var error = await deleteResponse.GetErrors();
            error.Message.Should().Be(ErrorMessageConstants.NotAValidProjectIdMessage);
        }
    }
}