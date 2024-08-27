using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expiresIn")]
        public string ExpiresIn { get; set; }

        [JsonConstructor]
        public LoginResponse(string accessToken, string refreshToken, string expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
        }
    }
}
