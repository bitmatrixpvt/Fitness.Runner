using System.Net.Http.Headers;
using System.Text;

namespace GymAppRunner;
public static class XeroInvoiceSyncRunner
{
    private static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzaHJ6aHVzc2FpbkB5YWhvby5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTeXN0ZW0gQWRtaW5pc3RyYXRvciIsIkxhbmd1YWdlIjoiZW4iLCJVc2VySWQiOiIxIiwiRW1wbG95ZWVJZCI6IjAiLCJDbHViSWRzIjoiMSwyLDMsNiw3LDgsOSwxMiwxNSwxNiwxNywxOCwxOSwyMiwyMywyNCwyNSwyNiwyNywyOCwyOSwzMCwzMSwzMywzNCwzNSwzOSw0MCw0MSw0Miw0Myw0Niw0Nyw0OSw1MCw1MSw1Miw1Myw1NCw1NSw1Niw1Nyw1OCw1OSw2MCw2MSw2Miw2Myw2Niw2Nyw2OCw2OSw3MCw4MCw4MSw4Miw4Myw4NCw4NSw4Niw4Nyw4OCIsIlJvbGVJZCI6IjEiLCJDb21wYW55SWQiOiIxIiwiQXBwbGljYXRpb25JZCI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiOiJTVVBFUiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJBRE1JTiIsImV4cCI6MTc5MDkyODc5NiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDozMDAwIn0.shy_uPlpNAVjs8vg_vrBBMX2uygValkR0Fe3yhdCV4s";
    private static string apiUrl = "http://localhost:5053/api/Xero/SyncBulkInvoice";
    // private static string apiUrl = "https://service9roundksa.bitmatrix.lk/api/Xero/SyncBulkInvoice";


    private static List<string> referenceIds = new()
    {
"JESMF_20260928_001"
    };

    public static async Task Run()
    {
        using HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromMinutes(10);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        for (int i = 0; i < referenceIds.Count; i++)
        {
            var referenceId = referenceIds[i];

            Console.WriteLine((i + 1).ToString() +" of " +referenceIds.Count.ToString() +" : " +referenceId);

            var postData = new StringContent("{\"InvoiceNo\":\"" + referenceId + "\"}",Encoding.UTF8,"application/json");
            var response = await client.PostAsync(apiUrl, postData);

            Console.WriteLine($"Status: {(int)response.StatusCode} {response.StatusCode}");

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
    }
}
