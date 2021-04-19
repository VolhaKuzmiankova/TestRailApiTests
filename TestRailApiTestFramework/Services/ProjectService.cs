using System.Net.Http;
using System.Threading.Tasks;
using TestRail.Extension;
using TestRail.Models;

namespace TestRail.Services
{
    public class ProjectService
    {
        private readonly HttpClient _client;

        public ProjectService(HttpClient client)
        {
            _client = client;
        }

        public Task<HttpResponseMessage> AddProject(CreateProjectModel createProjectModel)
        {
            return _client.PostAsJsonAsync("index.php?/api/v2/add_project", createProjectModel);
        }

        public Task<HttpResponseMessage> GetProject(int projectId)
        {
            return _client.GetAsync($"index.php?/api/v2/get_project/{projectId}");
        }

        public Task<HttpResponseMessage> Delete(int projectId)
        {
            return _client.PostAsync($"index.php?/api/v2/delete_project/{projectId}");
        }
    }
}