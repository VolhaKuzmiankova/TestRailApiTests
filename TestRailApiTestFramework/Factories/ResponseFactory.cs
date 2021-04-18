using TestRail.Models;

namespace TestRail.Factories
{
    public static class ResponseFactory
    {
        public static ProjectResponse GetProjectResponse(CreateProjectModel createProjectModel)
        {
            return new ProjectResponse()
            {
                Name = createProjectModel.Name,
                Announcement = createProjectModel.Announcement,
                ShowAnnouncement = createProjectModel.ShowAnnouncement,
                SuiteMode = 3,
                IsCompleted = false
            };
        }

        public static SuiteResponse GetSuiteResponse(CreateSuiteModel createSuiteModel)
        {
            return new SuiteResponse()
            {
                Name = createSuiteModel.Name,
                Description = createSuiteModel.Description,
                Id = createSuiteModel.Id
            };
        }
    }
}