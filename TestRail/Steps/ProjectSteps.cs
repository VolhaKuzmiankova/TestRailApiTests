using System.Net;
using System.Threading.Tasks;
using TestRail.Extension;
using TestRail.Models;
using TestRail.Services;

namespace TestRail.Steps
{
    public class ProjectSteps
    {
        private readonly ProjectService _projectService;

        public ProjectSteps(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<ProjectResponse> AddProject(CreateProjectModel createCreateProjectModel)
        {
            var response = await _projectService.AddProject(createCreateProjectModel);
            response.ResponseStatusCode(HttpStatusCode.OK, "Incorrect step when add project.");
            return await response.GetContentModel<ProjectResponse>();
        }

        public async Task<ProjectResponse> GetProject(int id)
        {
            var response = await _projectService.GetProject(id);
            response.ResponseStatusCode(HttpStatusCode.OK, "Incorrect step when get project." );
            return await response.GetContentModel<ProjectResponse>();
        }
    }
}