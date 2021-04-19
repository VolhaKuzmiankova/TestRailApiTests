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
    public class AddSuiteTests : BaseTest
    {
        public AddSuiteTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_suite/{projectId} when fields have correct values returns 200")]
        public async Task AddSuite_WhenFieldsHaveCorrectValues_ShouldReturnOK_ShouldReturnOk()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.AddSuite(project.Id, suiteModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseModel = await response.GetContentModel<SuiteResponse>();
            var expectedResponse = ResponseFactory.SuiteResponseModel(suiteModel);
            SuiteAssertion.AssertSuite(expectedResponse, responseModel);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_suite/{projectId} when user is unauthorized returns 401")]
        public async Task CreateProjectWhenUnAuthorized_ShouldReturnUnauthorized()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var suiteModel = SuiteFactory.GetSuiteModel();
            ClearAuthorization();

            //Act
            var response = await _suiteService.AddSuite(project.Id, suiteModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            var error = await response.GetContentModel<Error>();
            error.Message.Should()
                .Be(ErrorMessageConstants.AuthenticationFailedMessage);
        }

        [AllureXunitTheory(DisplayName = "POST index.php?/api/v2/add_suite/{projectId} when projectId is incorrect returns 400")]
        [MemberData(nameof(ProjectMocks.ProjectIncorrectValues), MemberType = typeof(ProjectMocks))]
        public async Task AddSuite_WhenProjectIdIsIncorrect_ShouldReturnBadRequest(int id, string message)
        {
            //Arrange
            SetUpAuthorization();
            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.AddSuite(id, suiteModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(message);
        }
    }
}