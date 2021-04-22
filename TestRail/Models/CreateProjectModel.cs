using Newtonsoft.Json;

namespace TestRail.Models
{
    public class CreateProjectModel
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("announcement")] public string Announcement { get; set; }

        [JsonProperty("show_announcement")] public bool ShowAnnouncement { get; set; }
    }
}