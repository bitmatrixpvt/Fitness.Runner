using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GymAppRunner;
public static class XeroInvoicePaymentSyncRunner
{
    private static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzaHJ6aHVzc2FpbkB5YWhvby5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTeXN0ZW0gQWRtaW5pc3RyYXRvciIsIkxhbmd1YWdlIjoiZW4iLCJVc2VySWQiOiIxIiwiRW1wbG95ZWVJZCI6IjAiLCJDbHViSWRzIjoiMSwyLDMsNiw3LDgsOSwxMiwxNSwxNiwxNywxOCwxOSwyMiwyMywyNCwyNSwyNiwyNywyOCwyOSwzMCwzMSwzMywzNCwzNSwzOSw0MCw0MSw0Miw0Myw0Niw0Nyw0OSw1MCw1MSw1Miw1Myw1NCw1NSw1Niw1Nyw1OCw1OSw2MCw2MSw2Miw2Myw2Niw2Nyw2OCw2OSw3MCw4MCw4MSw4Miw4Myw4NCw4NSw4Niw4Nyw4OCIsIlJvbGVJZCI6IjEiLCJDb21wYW55SWQiOiIxIiwiQXBwbGljYXRpb25JZCI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiOiJTVVBFUiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJBRE1JTiIsImV4cCI6MTc5MDY4NzgzMCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDozMDAwIn0.ZdLcx_oOgrKI8-Y7iRxoeI77rKgrIgtp4O6oLIOHWVE";
    private static string apiUrl = "http://localhost:5053/api/Xero/InvoicePayments/Sync";
    //private static string apiUrl = "https://service9roundksa.bitmatrix.lk/api/Xero/InvoicePayments/Sync";


    private static List<string> referenceIds = new()
    {
"JEHDF_20260908_001",
"RYYNF_20260908_001",
"RYYNF_20260907_001",
"RYYNF_20260906_001",
"KBRKF_20260906_001",
"RYRBM_20260905_001",
"TFRDM_20260901_001",
"MAWM_20260901_001"
    };

    public static async Task Run()
    {
        using HttpClient client = new HttpClient();

        client.Timeout = TimeSpan.FromMinutes(5);

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        const int batchSize = 10;
        const int delayBetweenBatchesSeconds = 2;

        for (int i = 0; i < referenceIds.Count; i++)
        {
            var referenceId = referenceIds[i];

            Console.WriteLine(
                $"{i + 1} of {referenceIds.Count} : {referenceId}");

            var success = await ProcessInvoice(
                client,
                referenceId);

            if (success)
            {
                Console.WriteLine("SUCCESS");
            }
            else
            {
                Console.WriteLine("FAILED");
            }

            // Wait after every batch
            if ((i + 1) % batchSize == 0 &&
                i + 1 < referenceIds.Count)
            {
                Console.WriteLine(
                    $"Waiting {delayBetweenBatchesSeconds} seconds before next batch...");

                await Task.Delay(
                    TimeSpan.FromSeconds(delayBetweenBatchesSeconds));
            }
        }

        Console.WriteLine("Finished.");
    }

    private static async Task<bool> ProcessInvoice(HttpClient client,string referenceId)
    {
        var request = new
        {
            InvoiceNo = referenceId
        };

        var json = JsonSerializer.Serialize(request);

        for (int attempt = 1; attempt <= 5; attempt++)
        {
            using var postData = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                apiUrl,
                postData);

            var responseBody =
                await response.Content.ReadAsStringAsync();

            Console.WriteLine(
                $"Status: {(int)response.StatusCode} {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(responseBody);
                return true;
            }

            // Too Many Requests
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                var retryAfter = response.Headers.RetryAfter;

                var delaySeconds =
                    retryAfter?.Delta?.TotalSeconds ??
                    Math.Pow(2, attempt) * 2;

                Console.WriteLine(
                    $"429 Too Many Requests. " +
                    $"Waiting {delaySeconds:0} seconds. " +
                    $"Attempt {attempt}/5");

                await Task.Delay(
                    TimeSpan.FromSeconds(delaySeconds));

                continue;
            }

            Console.WriteLine(responseBody);

            return false;
        }

        return false;
    }
}
