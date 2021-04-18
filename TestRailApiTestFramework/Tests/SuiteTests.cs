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
    public class SuiteTests : BaseTest
    {
        public SuiteTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_suite/{projectId} when returns 200")]
        public async Task AddSuite_ShouldReturnOk()
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
            var expectedResponse = ResponseFactory.GetSuiteResponse(suiteModel);
            SuiteAssertion.AssertSuite(expectedResponse, responseModel);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/add_suite/{projectId} when unauthorized returns 401")]
        public async Task CreateProjectWhenNotAuthorized_ShouldReturnUnauthorized()
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
                .Be("Authentication failed: invalid or missing user/password or session cookie.");
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/add_suite/{projectId} when projectId is incorrect returns 400")]
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


        [AllureXunit(DisplayName = "POST index.php?/api/v2/update_suite/{suiteId} when returns 200")]
        public async Task UpdateSuite_ShouldReturnOk()
        {
            //Arrange
            SetUpAuthorization();
            var project = await _projectSteps.AddProject(ProjectFactory.GetProjectModel());
            var createdSuite = await _suiteSteps.AddSuite(project.Id, SuiteFactory.GetSuiteModel());

            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.UpdateSuite(createdSuite.Id, suiteModel);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseSuite = await response.GetContentModel<SuiteResponse>();
            var expectedSuite = ResponseFactory.GetSuiteResponse(suiteModel);
            SuiteAssertion.AssertSuite(expectedSuite, responseSuite);
        }

        [AllureXunit(DisplayName = "POST index.php?/api/v2/update_suite/{suiteId} when unauthorized returns 401")]
        public async Task UpdateSuiteWhenNotAuthorized_ShouldReturnUnauthorized()
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
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be("Authentication failed: invalid or missing user/password or session cookie.");
        }

        [AllureXunitTheory(DisplayName =
            "POST index.php?/api/v2/update_suite/{suiteId} when suiteId is incorrect returns 400")]
        [MemberData(nameof(SuiteMocks.IncorrectValues), MemberType = typeof(SuiteMocks))]
        public async Task UpdateSuite_WhenSuiteIdIsIncorrect_ShouldReturnBadRequest(int id, string message)
        {
            //Arrange
            SetUpAuthorization();
            var suiteModel = SuiteFactory.GetSuiteModel();

            //Act
            var response = await _suiteService.UpdateSuite(id, suiteModel);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.GetContentModel<Error>();
            error.Message.Should().Be(message);
        }
    }
}