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
    public class AddProjectTests : BaseTest
    {
        public AddProjectTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [AllureXunit(DisplayName =
            "POST index.php?/api/v2/add_project when required fields have correct values returns 200")]
        public async Task AddProject_WhenFieldsHaveCorrectValues_ShouldReturnOK()
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

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_project when user is unauthorized returns 401")]
        public async Task CreateProjectWhenUnAuthorized_ShouldReturnUnauthorized()
        {
            //Act
            var response = await _projectService.AddProject(ProjectFactory.GetProjectModel());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(ErrorMessageConstants.AuthenticationFailedMessage);
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/add_project when required field has incorrect values returns 400")]
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
    }
}