using Newtonsoft.Json;

namespace TestRail.Models
{
    public class SuiteResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("project_id")] public int ProjectId { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("is_completed")] public bool IsCompleted { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("is_master")] public bool IsMaster { get; set; }
    }
}