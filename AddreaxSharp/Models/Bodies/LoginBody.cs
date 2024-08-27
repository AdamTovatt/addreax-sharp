using System.Text.Json.Serialization;

namespace AddreaxSharp.Models.Bodies
{
    public class LoginBody
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonConstructor]
        public LoginBody(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
