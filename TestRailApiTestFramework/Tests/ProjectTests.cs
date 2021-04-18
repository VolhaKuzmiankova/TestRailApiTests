using System.Net;
using System.Threading.Tasks;
using Allure.Xunit.Attributes;
using FluentAssertions;
using Newtonsoft.Json;
using TestRail.Assertion;
using TestRail.Extension;
using TestRail.Factories;
using TestRail.Mocks;
using TestRail.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestRail.Tests
{
    public class ProjectTests : BaseTest
    {
        public ProjectTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_project when returns 200")]
        public async Task AddProject_ShouldReturnOK()
        {
            //Arrange
            SetUpAuthorization();
            var projectModel = ProjectFactory.GetProjectModel();

            //Act
            var response = await _projectService.AddProject(projectModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseModel = await response.GetContentModel<ProjectResponse>();
            var expectedResponse = ResponseFactory.GetProjectResponse(projectModel);

            ProjectAssertion.AssertProject(expectedResponse, responseModel);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_project when unauthorized returns 401")]
        public async Task CreateProjectWhenNotAuthorized_ShouldReturnUnauthorized()
        {
            //Act
            var response = await _projectService.AddProject(ProjectFactory.GetProjectModel());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be("Authentication failed: invalid or missing user/password or session cookie.");
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/add_project when required field incorrect returns 400")]
        [MemberData(nameof(ProjectMocks.ProjectMissingValues), MemberType = typeof(ProjectMocks))]
        public async Task AddProjectWhenFieldHasIncorrectValue_ShouldReturnBadRequest(
            string serializedModel, string message)
        {
            //Arrange
            SetUpAuthorization();
            //TODO AllureXUnit can't work with structured parameters, so we serialized model to string and deserialized in test
            var createProjectModel = JsonConvert.DeserializeObject<CreateProjectModel>(serializedModel);

            //Act
            var response = await _projectService.AddProject(createProjectModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(message);
        }


        [AllureXunit(DisplayName = "GET index.php?/api/v2/get_project/{projectId} when returns 200")]
        public async Task GetProject_ShouldReturnOk()
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
            "GET index.php?/api/v2/get_project{projectId} when required field is incorrect returns 400")]
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

        [AllureXunit(DisplayName = "GET index.php?/api/v2/get_project/{projectId} when unauthorized returns 401")]
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
            error.Message.Should().Be("Authentication failed: invalid or missing user/password or session cookie.");
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/delete_project{projectId} when  returns 200")]
        public async Task DeleteProject_ShouldReturnOk()
        {
            //Arrange
            SetUpAuthorization();
            var projectModel = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());

            //Act
            var response = await _projectService.Delete(projectModel.Id);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/delete_project{projectId} when projectId is incorrect returns 400")]
        [MemberData(nameof(ProjectMocks.ProjectIncorrectValues), MemberType = typeof(ProjectMocks))]
        public async Task DeleteProject_WhenProjectIdIsIncorrectValue_ShouldReturnBadRequest(int id, string message)
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

        [AllureXunit(DisplayName = "POST index.php?/api/v2/delete_project{projectId} when unauthorized returns 401")]
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
            error.Message.Should().Be("Authentication failed: invalid or missing user/password or session cookie.");
        }
    }
}