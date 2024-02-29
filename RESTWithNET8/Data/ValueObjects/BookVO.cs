using System.Text.Json.Serialization;

namespace RESTWithNET8.Data.ValueObjects
{
    public class BookVO
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("launchDate")]
        public DateTime LaunchDate { get; set; }
    }
}
