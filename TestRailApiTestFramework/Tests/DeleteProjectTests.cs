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
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [AllureXunitTheory(DisplayName = "POST index.php?/api/v2/delete_project{projectId} when projectId is incorrect returns 400")]
        [MemberData(nameof(ProjectMocks.ProjectIncorrectValues), MemberType = typeof(ProjectMocks))]
        public async Task DeleteProject_WhenProjectIdIsIncorrect_ShouldReturnBadRequest(int id, string message)
        {
            //Arrange
            SetUpAuthorization();

            //Act
            var response = await _projectService.Delete(id);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.GetContentModel<Error>();
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
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(ErrorMessageConstants.AuthenticationFailedMessage);
        }
    }
}