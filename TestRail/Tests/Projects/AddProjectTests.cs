using System.Net;
using System.Threading.Tasks;
using Allure.Xunit.Attributes;
using FluentAssertions;
using Newtonsoft.Json;
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
            response.ResponseStatusCode(HttpStatusCode.OK, "Expected OK status.");
            var responseModel = await response.GetContentModel<ProjectResponse>();
            var expectedResponse = ResponseFactory.GetProjectResponse(projectModel);

            ProjectAssertion.AssertProject(expectedResponse, responseModel);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_project when user is unauthorized returns 401")]
        public async Task CreateProject_WhenUnAuthorized_ShouldReturnUnauthorized()
        {
            //Act
            var response = await _projectService.AddProject(ProjectFactory.GetProjectModel());

            //Assert
            response.ResponseStatusCode(HttpStatusCode.Unauthorized, "Expected Unauthorized status.");
            var error = await response.GetErrors();
            error.Message.Should().Be(ErrorMessageConstants.AuthenticationFailedMessage);
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/add_project when required field has incorrect value returns 400")]
        [MemberData(nameof(ProjectMocks.ProjectIncorrectValues), MemberType = typeof(ProjectMocks))]
        public async Task AddProject_WhenFieldHasIncorrectValue_ShouldReturnBadRequest(
            string serializeProject, string message)
        {
            //Arrange
            SetUpAuthorization();
            //TODO AllureXUnit can not work with structured parameters, so we serialized model to string and deserialized in test
            var createProjectModel = JsonConvert.DeserializeObject<CreateProjectModel>(serializeProject);

            //Act
            var response = await _projectService.AddProject(createProjectModel);
            
            //Assert
            response.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
            var error = await response.GetErrors();
            error.Message.Should().Be(message);
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/add_project when required field was missed value returns 400")]
        [MemberData(nameof(ProjectMocks.ProjectMissingValues), MemberType = typeof(ProjectMocks))]
        public async Task AddProject_WhenRequiredFieldWasMissed_ShouldReturnBadRequest(
            string serializeModel)
        {
            //Arrange
            SetUpAuthorization();
            //TODO AllureXUnit can not work with structured parameters, so we serialized model to string and deserialized in test
            var createProjectModel = JsonConvert.DeserializeObject<CreateProjectModel>(serializeModel);

            //Act
            var response = await _projectService.AddProject(createProjectModel);

            //Assert
            response.ResponseStatusCode(HttpStatusCode.BadRequest, "Expected BadRequest status.");
            var error = await response.GetErrors();
            error.Message.Should().Be(ErrorMessageConstants.MissingNameMessage);
        }
    }
}