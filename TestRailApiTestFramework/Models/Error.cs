using System;
using Newtonsoft.Json;

namespace TestRail.Models
{
    public class Error
    {
        [JsonProperty("error")] public string Message { get; set; }
    }
}