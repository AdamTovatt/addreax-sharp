using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class OccurenceTicketLink
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("organization_event_occurrence_id")]
        public int OrganizationEventOccurenceId { get; set; }

        [JsonPropertyName("tickets_id")]
        public int TicketsId { get; set; }

        public OccurenceTicketLink() { }

        public OccurenceTicketLink(int organizationEventOccurrenceId, int ticketsId, int? id = null)
        {
            OrganizationEventOccurenceId = organizationEventOccurrenceId;
            TicketsId = ticketsId;
            Id = id;
        }
    }
}
