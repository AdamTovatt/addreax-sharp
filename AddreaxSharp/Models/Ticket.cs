using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class Ticket
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("max_count_per_member")]
        public int MaxCountPerMember { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("segment_id")]
        public int SegmentId { get; set; }

        [JsonConstructor]
        public Ticket(
            string name,
            int count,
            DateTime startDate,
            DateTime endDate,
            int maxCountPerMember,
            int price,
            string currency,
            int type,
            int segmentId)
        {
            Name = name;
            Count = count;
            StartDate = startDate;
            EndDate = endDate;
            MaxCountPerMember = maxCountPerMember;
            Price = price;
            Currency = currency;
            Type = type;
            SegmentId = segmentId;
        }
    }
}
