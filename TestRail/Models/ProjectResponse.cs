using Newtonsoft.Json;

namespace TestRail.Models
{
    public class ProjectResponse
    {
        [JsonProperty("announcement")] public string Announcement { get; set; }

        [JsonProperty("is_completed")] public bool IsCompleted { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("show_announcement")] public bool ShowAnnouncement { get; set; }

        [JsonProperty("suite_mode")] public int SuiteMode { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
        
        [JsonProperty("completed_on")] public string CompletedOn { get; set; }
    }
}