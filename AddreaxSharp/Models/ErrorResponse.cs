using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonConstructor]
        public ErrorResponse(int statusCode, string name, string message)
        {
            StatusCode = statusCode;
            Name = name;
            Message = message;
        }
    }

    public class WrappedErrorResponse
    {
        [JsonPropertyName("error")]
        public ErrorResponse Error { get; set; }

        [JsonConstructor]
        public WrappedErrorResponse(ErrorResponse error)
        {
            Error = error;
        }
    }
}
