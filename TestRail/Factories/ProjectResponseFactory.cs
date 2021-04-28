using TestRail.Constants;
using TestRail.Models;

namespace TestRail.Factories
{
    public static class ProjectResponseFactory
    {
        public static ProjectResponse GetProjectResponse(CreateProjectModel createProjectModel)
        {
            return new ProjectResponse()
            {
                Name = createProjectModel.Name,
                Announcement = createProjectModel.Announcement,
                ShowAnnouncement = createProjectModel.ShowAnnouncement,
                SuiteMode = SuiteModeConstants.SuiteModeValue,
                IsCompleted = false
            };
        }


    }
}