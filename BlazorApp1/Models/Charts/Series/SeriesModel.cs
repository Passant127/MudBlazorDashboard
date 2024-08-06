using System.Text.Json.Serialization;

namespace BlazorApp1.Models.Charts.Series;

public class SeriesModel<TSeries>
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("data")] public List<TSeries> Data { get; set; }
}