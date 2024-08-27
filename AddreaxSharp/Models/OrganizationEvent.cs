using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AddreaxSharp.Models
{
    public class OrganizationEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("organization_event_occurrences")]
        public List<EventOccurence> EventOccurrences { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("image_page_url")]
        public string ImagePageUrl { get; set; }

        [JsonConstructor]
        public OrganizationEvent(int id, string title, string content, List<EventOccurence> eventOccurrences, string imageUrl, string imagePageUrl)
        {
            Id = id;
            Title = title;
            Content = content;
            EventOccurrences = eventOccurrences;
            ImageUrl = imageUrl;
            ImagePageUrl = imagePageUrl;
        }

        public EventOccurence GetLastestOccurence()
        {
            return EventOccurrences.OrderByDescending(x => x.StartDate).First();
        }
    }
}
