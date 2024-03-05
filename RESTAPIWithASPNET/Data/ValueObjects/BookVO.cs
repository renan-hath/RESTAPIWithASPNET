using RESTWithNET8.Hypermedia;
using RESTWithNET8.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace RESTWithNET8.Data.ValueObjects
{
    public class BookVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("launchDate")]
        public DateTime LaunchDate { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
