using System.Text;

namespace GymAppRunner;
public static class PaymentRunner
{
    private static string apiUrl = "http://localhost:5053/api/ExternalPayments/Proceed";
    private static List<string> referenceIds = new()
    {
"e2ead204-4a56-49eb-9167-b9e0cf252c4d",
"9e6e1803-52c5-4b31-bfeb-4930772a3a5d",
"6319b7ad-d76d-4505-b295-ed919466e9a6",
"36a8aec4-36e5-4fa2-8e7c-26a332a68c8a",
"ee3e479b-5de1-4364-94b5-b4b9dfe8d3ab",
"2e63a27b-86d6-4cfa-9fe2-7551aac656a3",
"8461d92b-ebd9-46a6-8c91-eb23a16922ce",
"ccdd75d0-ff98-4e09-b857-efad7b83f493",
"9245cbe5-0b09-4071-89bf-6b600edbdde0",
"5472229f-f830-46bf-a581-b829f84cb873",
"f5f855c8-410e-4085-b41d-4ddce1da0d1b",
"051a6f20-67de-462f-900b-1a61ddda496c",
"cee0c9eb-9378-4651-b70f-20e8fdac6fd0",
"f9e8b176-4ecd-4f54-a3d6-1c7a8d4e619f",
"d4efe938-329d-4e2b-80cb-928b9b9e1529"
    };

    public static async Task Run()
    {
        using HttpClient client = new HttpClient();

        for (int i = 0; i < referenceIds.Count; i++)
        {
            var referenceId = referenceIds[i];
            Console.WriteLine((i + 1).ToString() + " of " + referenceIds.Count.ToString() + " : " + referenceId);
            var postData = new StringContent(
            "{\"ExternalReferenceId\":\"" + referenceId + "\"}",
            Encoding.UTF8,
            "application/json");
            await client.PostAsync(apiUrl, postData);
        }
    }
}
