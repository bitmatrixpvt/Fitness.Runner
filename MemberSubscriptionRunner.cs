using System.Net.Http.Headers;
using System.Text;

namespace GymAppRunner;
public static class MemberSubscriptionRunner
{
    private static string apiUrl = "https://service9roundksa.bitmatrix.lk/api/MemberSubscriptions";
    private static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzaHJ6aHVzc2FpbkB5YWhvby5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJTeXN0ZW0gQWRtaW5pc3RyYXRvciIsIkxhbmd1YWdlIjoiZW4iLCJVc2VySWQiOiIxIiwiRW1wbG95ZWVJZCI6IjAiLCJDbHViSWRzIjoiMSwyLDMsNiw3LDgsOSwxMiwxNSwxNiwxNywxOCwxOSwyMiwyMywyNCwyNSwyNiwyNywyOCwyOSwzMCwzMSwzMywzNCwzNSwzOSw0MCw0MSw0Miw0Myw0Niw0Nyw0OSw1MCw1MSw1Miw1Myw1NCw1NSw1Niw1Nyw1OCw1OSw2MCw2MSw2Miw2Myw2Niw2Nyw2OCw2OSw3MCw4MCw4MSw4Miw4Myw4NCw4NSw4Niw4Nyw4OCIsIlJvbGVJZCI6IjEiLCJDb21wYW55SWQiOiIxIiwiQXBwbGljYXRpb25JZCI6IjIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiOiJTVVBFUiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3N1cm5hbWUiOiJBRE1JTiIsImV4cCI6MTc4Njg3NzI2OCwiaXNzIjoiaHR0cDovLzE1Ny41Ni4xODEuMjMxOjM2MzYiLCJhdWQiOiJodHRwOi8vMTU3LjU2LjE4MS4yMzE6MzYzNyJ9.f_lI0ynw3jBi_HFvW_pYhXIIFJbpmLuDG_0G1-Dejlk";
    private static int subscriptionPlanId = 10;
    private static int clubId = 84;
    private static string reason = "SP-4183: Jeddah, AlRawdah (Ladies) branch closing, Transfer All active members to Al Marwah 2.0 (Ladies) and additional 30 days";
    private static string subscriptionStartDate = "2026-08-16";

    private static List<int> memberList = new()
    {
839306
,999764
,1007634
,1087135
,1135336
,1155959
,1182546
,1199943
,1208068
,1524050
,1524135
,1527376
,1529548
,1530469
,1534831
,1535749
,1536886
,1537118
,1539158
,1539189
,1540747
,1542902
,823641
,825011
,831763
,838426
,1077273
,1113310
,1131151
,1132557
,1133359
,1148142
,1176316
,1187448
,1198712
,1210076
,1216060
,1519249
,1524304
,1527671
,1528048
,1530722
,1535804
,1537004
,1539314
,1539648
,1540439
,1541343
,1543347
,1544217
,1544985
,1545128
,1545364
,825060
,849677
,1061072
,1061523
,1073723
,1076680
,1108419
,1122827
,1129611
,1173863
,1178762
,1185012
,1186199
,1195976
,1202779
,1203562
,1509351
,1517956
,1532581
,1533249
,1535891
,1538431
,1539903
,1545098
,1545181
,1545183
,848555
,1008042
,1014811
,1085239
,1127572
,1127989
,1139840
,1158307
,1160328
,1205888
,1207439
,1208511
,1504336
,1529372
,1533767
,1534069
,1535273
,1538764
,1540168
,1542057
,1543997
,1544780
,817486
,820817
,833562
,839312
,841650
,1065898
,1069740
,1110218
,1137855
,1143880
,1153929
,1181046
,1191910
,1211730
,1247060
,1525424
,1526728
,1526753
,1529200
,1530331
,1530663
,1534146
,1536356
,1537826
,1539187
,1539696
,1539857
,1542538
,1543263
,1545180
,818952
,826553
,854598
,883242
,896378
,1057186
,1067582
,1071708
,1092168
,1102228
,1103246
,1110466
,1150110
,1204039
,1211124
,1513420
,1514956
,1522712
,1527241
,1531804
,1534111
,1535797
,1537002
,1540963
,1541178
,1542318
,1542717
,872349
,1085233
,1091896
,1110219
,1120847
,1145819
,1153796
,1158962
,1159598
,1160155
,1161770
,1196909
,1202439
,1211263
,1214246
,1504499
,1508250
,1532218
,1537241
,1539913
,1540174
,1540860
,1544911
,1545815
,1546440
,1546647
,826566
,845379
,845393
,1012310
,1071363
,1072949
,1073053
,1081041
,1094366
,1094559
,1100729
,1108181
,1112893
,1129280
,1133049
,1159271
,1188058
,1209382
,1245639
,1246779
,1505787
,1513343
,1521313
,1522649
,1525943
,1528969
,1530723
,1532952
,1537157
,1537239
,1538400
,1540651
,1543816
,1544909
    };

// SELECT* FROM Clubs WHERE Id=30
// SELECT DISTINCT MemberId FROM MemberSubscriptions WHERE SubscriptionPlanId!=36 AND Status = 1 AND ClubId=30 AND SubscriptionStatus IN('CURRENT','NOT_STARTED','FREEZED')

    public static async Task Run()
    {
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        for (int i = 0; i < memberList.Count; i++)
        {
            int memberId = memberList[i];
            Console.WriteLine((i + 1).ToString() + " of " + memberList.Count.ToString() + " : " + memberId.ToString());
            var postData = new StringContent(
            "{\"reason\":\"" + reason + "\",\"paymentMethod\":\"CASH\",\"subscriptionStartDate\":\"" + subscriptionStartDate + "\",\"memberId\":" + memberId + ",\"subscriptionPlanId\":" + subscriptionPlanId + ",\"clubId\":" + clubId + "}",
            Encoding.UTF8,
            "application/json");
            await client.PostAsync(apiUrl, postData);
        }
    }
}
