using System.Text.Json.Serialization;

namespace AddreaxSharp.Models.Bodies
{
    public class LinkTicketsToOccurenceBody
    {
        [JsonPropertyName("organization_event_id")]
        public int OrganizationEventId { get; set; }

        [JsonPropertyName("tickets_id")]
        public int TicketsId { get; set; }

        [JsonConstructor]
        public LinkTicketsToOccurenceBody(int organizationEventId, int ticketsId)
        {
            OrganizationEventId = organizationEventId;
            TicketsId = ticketsId;
        }
    }
}
