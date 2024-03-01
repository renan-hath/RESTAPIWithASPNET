using RESTWithNET8.Hypermedia;
using RESTWithNET8.Hypermedia.Abstract;
using RESTWithNET8.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RESTWithNET8.Data.ValueObjects
{
    public class PersonVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        public string Gender {  get; set; } = string.Empty;

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
