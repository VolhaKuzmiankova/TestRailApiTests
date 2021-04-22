using System.Net.Http;
using System.Threading.Tasks;
using TestRail.Extension;
using TestRail.Models;

namespace TestRail.Services
{
    public class SuiteService
    {
        private readonly HttpClient _client;

        public SuiteService(HttpClient client)
        {
            _client = client;
        }

        public Task<HttpResponseMessage> AddSuite(int projectId, CreateSuiteModel createSuiteModel)
        {
            return AddSuite(projectId.ToString(), createSuiteModel);
        }
        
        public Task<HttpResponseMessage> AddSuite(string projectId, CreateSuiteModel createSuiteModel)
        {
            return _client.PostAsJsonAsync($"index.php?/api/v2/add_suite/{projectId}", createSuiteModel);
        }
        

        public Task<HttpResponseMessage> UpdateSuite(int suiteId, CreateSuiteModel createSuiteModel)
        {
            return UpdateSuite(suiteId.ToString(), createSuiteModel);
        }
        
        public Task<HttpResponseMessage> UpdateSuite(string suiteId, CreateSuiteModel createSuiteModel)
        {
            return _client.PostAsJsonAsync($"index.php?/api/v2/update_suite/{suiteId}", createSuiteModel);
        }
    }
}