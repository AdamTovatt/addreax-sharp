using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class UserInformation
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("organization_id")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("organization_name")]
        public string OrganizationName { get; set; }

        [JsonConstructor]
        public UserInformation(int id, int organizationId, string email, string organizationName)
        {
            Id = id;
            OrganizationId = organizationId;
            Email = email;
            OrganizationName = organizationName;
        }
    }
}
