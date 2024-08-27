using AddreaxSharp;
using AddreaxSharp.Models;
using AddreaxSharp.Models.Bodies;
using System.Text.Json;

namespace AddreaxConsole
{
    internal class Program
    {
        private const string credentialsFileName = "credentials.txt";

        static async Task Main(string[] args)
        {
            Addreax addreax = new Addreax(GetLoginBody());

            await addreax.LoginAsync();

            ApiResponse<UserInformation> userInformationResponse = await addreax.GetUserInformationAsync();

            if (userInformationResponse.Data == null)
            {
                Console.WriteLine(userInformationResponse.Error!.Message);
                Console.ReadLine();
                Console.ReadLine();
                return;
            }

            ApiResponse<List<EventSummary>> organizationEventsResponse = await addreax.GetOrganizationEvents(userInformationResponse.Data.OrganizationId);

            if (organizationEventsResponse.Data == null)
            {
                Console.WriteLine(organizationEventsResponse.Error!.Message);
                Console.ReadLine();
                return;
            }

            organizationEventsResponse.Data = organizationEventsResponse.Data.OrderByDescending(x => x.UpdatedAt).ToList();

            foreach (EventSummary summary in organizationEventsResponse.Data)
            {
                Console.WriteLine(summary.Id + " " + summary.Title);
            }

            Console.WriteLine("\nEnter id:");

            int eventId = int.Parse(Console.ReadLine()!);

            ApiResponse<OrganizationEvent> eventResponse = await addreax.GetOrganizationEvent(eventId);

            if (eventResponse.Data == null)
            {
                Console.WriteLine(eventResponse.Error!);
                Console.ReadLine();
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                List<ErrorResponse> cloningErrors = await CloneEventOccurrenceAsync(eventResponse.Data!.GetLastestOccurence(), addreax);

                foreach (ErrorResponse error in cloningErrors)
                {
                    Console.WriteLine(error.Message);
                }

                eventResponse = await addreax.GetOrganizationEvent(eventId);
            }

            Console.WriteLine("cloned");

            Console.ReadLine();
        }

        private static async Task<List<ErrorResponse>> CloneEventOccurrenceAsync(EventOccurence eventOccurence, Addreax addreax)
        {
            List<ErrorResponse> result = new List<ErrorResponse>();

            Ticket? tickets = eventOccurence.Tickets?.FirstOrDefault();

            eventOccurence.StartDate = eventOccurence.StartDate.AddDays(7);
            eventOccurence.EndDate = eventOccurence.EndDate.AddDays(7);

            ApiResponse<EventOccurence> clonedOccurrenceResponse = await addreax.CreateEventOccurrenceAsync(eventOccurence);

            if (clonedOccurrenceResponse.Error != null)
            {
                result.Add(clonedOccurrenceResponse.Error);
                return result;
            }

            if (tickets != null)
            {
                tickets.StartDate = eventOccurence.StartDate.AddDays(-14);
                tickets.EndDate = eventOccurence.StartDate;

                ApiResponse<Ticket> clonedTicketsResponse = await addreax.CreateTicketAsync(tickets);

                if (clonedTicketsResponse.Error != null)
                {
                    result.Add(clonedTicketsResponse.Error);
                    return result;
                }

                OccurenceTicketLink link = new OccurenceTicketLink((int)clonedOccurrenceResponse.Data!.Id!, (int)clonedTicketsResponse.Data!.Id!);
                ApiResponse<OccurenceTicketLink> linkResponse = await addreax.LinkOccurrenceAndTicketAsync(link);

                if (linkResponse.Error != null)
                {
                    result.Add(linkResponse.Error);
                    return result;
                }
            }

            return result;
        }

        private static LoginBody GetLoginBody()
        {
            if (File.Exists(credentialsFileName))
            {
                return JsonSerializer.Deserialize<LoginBody>(File.ReadAllText(credentialsFileName))!;
            }
            else
            {
                Console.WriteLine("username:");
                string username = Console.ReadLine()!;

                Console.WriteLine("password:");
                string password = Console.ReadLine()!;

                string json = JsonSerializer.Serialize(new LoginBody(username, password));
                File.WriteAllText(credentialsFileName, json);

                return new LoginBody(username, password);
            }
        }
    }
}
