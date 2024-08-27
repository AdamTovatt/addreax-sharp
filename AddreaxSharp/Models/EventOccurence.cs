using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class EventOccurence
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("organization_event_id")]
        public int OrganizationEventId { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("zip_code")]
        public string ZipCode { get; set; }

        [JsonPropertyName("street_address")]
        public string StreetAddress { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("deep_link")]
        public string? DeepLink { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("tickets")]
        public List<Ticket>? Tickets { get; set; }

        [JsonConstructor]
        public EventOccurence(int organizationEventId, string location, DateTime startDate, DateTime endDate, string zipCode, string streetAddress, string city)
        {
            OrganizationEventId = organizationEventId;
            Location = location;
            StartDate = startDate;
            EndDate = endDate;
            ZipCode = zipCode;
            StreetAddress = streetAddress;
            City = city;
        }
    }
}
