### C# library for the Addreax platform

Example usage:
```
Addreax addreax = new Addreax("email", "password");
await addreax.LoginAsync();

ApiResponse<UserInformation> userInformationResponse = await addreax.GetUserInformationAsync();

if (userInformationResponse.Data == null)
{
    Console.WriteLine(userInformationResponse.Error!.Message);
    return;
}

ApiResponse<List<EventSummary>> organizationEventsResponse = await addreax.GetOrganizationEvents(userInformationResponse.Data.OrganizationId);
```
