using Newtonsoft.Json;

namespace TestRail.Models
{
    public class CreateSuiteModel
    {
        [JsonProperty("id")] public int Id { get; set; }
        
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}