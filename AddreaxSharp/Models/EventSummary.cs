using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class EventSummary
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonConstructor]
        public EventSummary(int id, string title, DateTime updatedAt)
        {
            Id = id;
            Title = title;
            UpdatedAt = updatedAt;
        }
    }
}
