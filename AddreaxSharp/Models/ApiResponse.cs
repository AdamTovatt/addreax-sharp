using System.Net;
using System.Text.Json.Serialization;

namespace AddreaxSharp.Models
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ErrorResponse? Error { get; set; }

        [JsonConstructor]
        public ApiResponse(T? data, bool success, HttpStatusCode statusCode, ErrorResponse? error)
        {
            Data = data;
            Success = success;
            StatusCode = statusCode;
            Error = error;
        }
    }
}
