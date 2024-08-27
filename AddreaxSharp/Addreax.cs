using AddreaxSharp.Models;
using System.Text.Json;
using System.Text;
using AddreaxSharp.Models.Bodies;
using System.Text.Json.Serialization;

namespace AddreaxSharp
{
    public class Addreax
    {
        private const string baseUrl = "https://api.stuk.co";

        public string? AccessToken { get; set; }

        private static readonly HttpClient httpClient = new HttpClient();

        private string email;
        private string password;

        public Addreax(LoginBody loginBody)
        {
            email = loginBody.Email;
            password = loginBody.Password;
        }

        public Addreax(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public async Task<ApiResponse<LoginResponse>> LoginAsync()
        {
            const string endpointPath = "/user/login";

            LoginBody requestBody = new LoginBody(email, password);

            ApiResponse<LoginResponse> response = await SendHttpRequestAsync<LoginBody, LoginResponse>(HttpMethod.Post, baseUrl + endpointPath, requestBody, isLogin: true);

            if (response.Success && response.Data != null)
                AccessToken = response.Data.AccessToken;

            return response;
        }

        public async Task<ApiResponse<UserInformation>> GetUserInformationAsync()
        {
            const string endpointPath = "/users/me";

            ApiResponse<UserInformation> response = await SendHttpRequestAsync<object, UserInformation>(HttpMethod.Get, baseUrl + endpointPath, null);
            return response;
        }

        public async Task<ApiResponse<List<EventSummary>>> GetOrganizationEvents(int organizationId)
        {
            string endpointPath = $"/organizations/{organizationId}/organization-events";

            ApiResponse<List<EventSummary>> response = await SendHttpRequestAsync<object, List<EventSummary>>(HttpMethod.Get, baseUrl + endpointPath, null);
            return response;
        }

        public async Task<ApiResponse<OrganizationEvent>> GetOrganizationEvent(int eventId)
        {
            string endpointPath = $"/organization-events/{eventId}" + """?filter={"include":[{"relation":"organization","scope":{"fields":["id","name","display_name"]}},{"relation":"organization_event_occurrences","scope":{"include":[{"relation":"tickets","scope":{"include":[{"relation":"forms"}]}},{"relation":"organization_event_occurrence_guests"}],"order":["start_date DESC"]}},{"relation":"organization_event_tags","scope":{"include":[{"relation":"organization_tag"}]}}]}""";

            ApiResponse<OrganizationEvent> response = await SendHttpRequestAsync<object, OrganizationEvent>(HttpMethod.Get, baseUrl + endpointPath, null);
            return response;
        }

        public async Task<ApiResponse<EventOccurence>> CreateEventOccurrenceAsync(EventOccurence eventOccurence)
        {
            const string endpointPath = "/organization-event-occurrences";

            eventOccurence.DeepLink = null;
            eventOccurence.Id = null;
            eventOccurence.Tickets = null;

            ApiResponse<EventOccurence> response = await SendHttpRequestAsync<EventOccurence, EventOccurence>(HttpMethod.Post, baseUrl + endpointPath, eventOccurence);
            return response;
        }

        public async Task<ApiResponse<Ticket>> CreateTicketAsync(Ticket ticket)
        {
            const string endpointPath = "/tickets";

            ticket.Id = null;

            ApiResponse<Ticket> response = await SendHttpRequestAsync<Ticket, Ticket>(HttpMethod.Post, baseUrl + endpointPath, ticket);
            return response;
        }

        public async Task<ApiResponse<OccurenceTicketLink>> LinkOccurrenceAndTicketAsync(OccurenceTicketLink link)
        {
            const string endpointPath = "/event-occurrence-tickets";

            ApiResponse<OccurenceTicketLink> response = await SendHttpRequestAsync<OccurenceTicketLink, OccurenceTicketLink>(HttpMethod.Post, baseUrl + endpointPath, link);
            return response;
        }

        private async Task<ApiResponse<TResponse>> SendHttpRequestAsync<TRequest, TResponse>(HttpMethod method, string url, TRequest? requestBody, bool isLogin = false)
        {
            if (!isLogin && string.IsNullOrEmpty(AccessToken))
                throw new ArgumentNullException($"{nameof(AccessToken)} can not be null when making a request!");

            HttpRequestMessage request = new HttpRequestMessage(method, url);

            if (!isLogin)
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

            if (requestBody != null && method != HttpMethod.Get)
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                string jsonContent = JsonSerializer.Serialize(requestBody, options);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TResponse? data = JsonSerializer.Deserialize<TResponse>(jsonResponse);

                if (data == null)
                    throw new Exception("Request completed but response data could not be deserialized. This was the json: " + jsonResponse);

                return new ApiResponse<TResponse>(data, true, response.StatusCode, null);
            }
            else
            {
                WrappedErrorResponse? errorResponse = JsonSerializer.Deserialize<WrappedErrorResponse>(jsonResponse);

                if (errorResponse == null)
                    throw new Exception("Request completed with an error and error response could not be deserialized. This was the json: " + jsonResponse);

                return new ApiResponse<TResponse>(default, false, response.StatusCode, errorResponse.Error);
            }
        }
    }
}
