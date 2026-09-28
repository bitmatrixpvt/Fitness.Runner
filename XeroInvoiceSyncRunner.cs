using System.Net.Http.Headers;
using System.Text;

namespace GymAppRunner;
public static class XeroInvoiceSyncRunner
{
    private static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzaHJ6aHVzc2FpbkB5YWhvby5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTeXN0ZW0gQWRtaW5pc3RyYXRvciIsIkxhbmd1YWdlIjoiZW4iLCJVc2VySWQiOiIxIiwiRW1wbG95ZWVJZCI6IjAiLCJDbHViSWRzIjoiMSwyLDMsNiw3LDgsOSwxMiwxNSwxNiwxNywxOCwxOSwyMiwyMywyNCwyNSwyNiwyNywyOCwyOSwzMCwzMSwzMywzNCwzNSwzOSw0MCw0MSw0Miw0Myw0Niw0Nyw0OSw1MCw1MSw1Miw1Myw1NCw1NSw1Niw1Nyw1OCw1OSw2MCw2MSw2Miw2Myw2Niw2Nyw2OCw2OSw3MCw4MCw4MSw4Miw4Myw4NCw4NSw4Niw4Nyw4OCIsIlJvbGVJZCI6IjEiLCJDb21wYW55SWQiOiIxIiwiQXBwbGljYXRpb25JZCI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiOiJTVVBFUiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJBRE1JTiIsImV4cCI6MTc5MDQyMzMyMCwiaXNzIjoiaHR0cDovLzE1Ny41Ni4xODEuMjMxOjM2MzYiLCJhdWQiOiJodHRwOi8vMTU3LjU2LjE4MS4yMzE6MzYzNyJ9.CXvZ-JRxv_KOVXzRdu0XrP2Tz_Wq6D-TtZ9pFuSwjHY";
    // private static string apiUrl = "http://localhost:5053/api/Xero/Invoice/Sync";
    private static string apiUrl = "https://service9roundksa.bitmatrix.lk/api/Xero/Invoice/Sync";


    private static List<string> referenceIds = new()
    {
"TFRDF_20260824_001",
"RYYNF_20260824_001",
"RYYKM_20260824_001",
"RYWHF_20260824_001",
"RYTNM_20260824_001",
"RYTNF_20260824_001",
"RYSWM_20260824_001",
"RYRYM_20260824_001",
"RYRDF_20260824_001",
"RYRBF_20260824_001",
"RYQWF_20260824_001",
"RYQTM_20260824_001",
"RYQTF_20260824_001",
"RYOLF_20260824_001",
"RYNKM_20260824_001",
"RYNDM_20260824_001",
"RYMTF_20260824_001",
"RYMQM_20260824_001",
"RYLBM_20260824_001",
"MAWM_20260824_001",
"KBRKM_20260824_001",
"JESMM_20260824_001",
"JESMF_20260824_001",
"JEOHF_20260824_001",
"JEMWM_20260824_001",
"JEMWF_20260824_001",
"JEHDM_20260824_001",
"JEHDF_20260824_001",
"JEFSM_20260824_001",
"JEBSF_20260824_001",
"HSSLM_20260824_001",
"DMFSF_20260824_001"
    };

    public static async Task Run()
    {
        using HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        for (int i = 0; i < referenceIds.Count; i++)
        {
            var referenceId = referenceIds[i];

            Console.WriteLine(
                (i + 1).ToString() +
                " of " +
                referenceIds.Count.ToString() +
                " : " +
                referenceId);

            var postData = new StringContent(
                "{\"InvoiceNo\":\"" + referenceId + "\"}",
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(apiUrl, postData);

            Console.WriteLine(
                $"Status: {(int)response.StatusCode} {response.StatusCode}");

            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
    }
}
