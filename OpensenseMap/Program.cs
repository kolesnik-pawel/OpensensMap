using OpensenseMap;
using RestSharp;
using Newtonsoft.Json;
using OpensenseMap.Models;
using System.Diagnostics.Metrics;

internal class Program
{
    //private static void Main(string[] args)
    //{
    //    Console.WriteLine("Hello, World!");
        //    Connection connection = new Connection("https://api.opensensemap.org/boxes");

        //    var request = new RestRequest("?bbox=20%2C52%2C21%2C52.5");

        //    Task<RestResponse> response = connection.Client.GetAsync(request);
        //    response.Wait();

        //    Console.WriteLine($"{response.Result.Content.ToString()}");  

        //    var deserialize = JsonConvert.DeserializeObject<List<Boxes>>(response.Result.Content.ToString());

        //    foreach (var item in deserialize)
        //    {
        //        Console.WriteLine(item._id);
        //        try
        //        {
        //            //foreach (var tag in item.grouptag)
        //            //{
        //            //    Console.WriteLine($"{tag.ToString()}");
        //            //}
        //        } catch(Exception ex) 
        //        {
        //            Console.WriteLine("brak tagu groupTag");
        //        }

        //    }

        //    //var request2 = new RestRequest("/data?boxId=5bb1dab2043f3f001ba31b69&from-date=2023-02-06T00:00:13.739Z&phenomenon=Temperatur");
        //    var request2 = new RestRequest("/data");

        //    var stream = new StreamReader("Requests/Measurements.json");
        //    //stream.ReadToEnd();
        //    //params object[] prameters = new object[];
        //    object[] prams = new object[10];


        //    string s = string.Format(new StreamReader("Requests/Measurements2.json").ReadToEnd()
        //        ,"json");
        //    //Console.WriteLine(s);   

        //    Console.WriteLine(stream.ReadToEnd());

        //    request2.AddStringBody(new StreamReader("Requests/Measurements.json").ReadToEnd(), "application/json");

        //    request2.AddParameter("bearer", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE0ODMwMDYxNTIsImV4cCI6MTQ4MzAwOTc1MiwiaXNzIjoibG9jYWxob3N0OjgwMDAiLCJzdWIiOiJ0ZXN0QHRlc3QuZGUiLCJqdGkiOiJmMjNiOThkNi1mMjRlLTRjOTQtYWE5Ni1kMWI4M2MzNmY1MjAifQ.QegeWHWetw19vfgOvkTCsBfaSOPnjakhzzRjVtNi-2Q");
        //   // RestResponse response2 = connection.Client.PostJson("/data", );        

        //    response = connection.Client.GetAsync(request2);

        //    response.Wait();
        //    var mesure = JsonConvert.DeserializeObject<List<BoxesDataMeasurementsReturn>>(response.Result.Content);
        //    foreach (var m in mesure)
        //    {
        //        Console.WriteLine($"{m.createdAt.ToString("dd-MM-yyyy HH:mm:ss")} temperatura : {m.value}");
        //    }
        //    Console.WriteLine(response.Result.Content);
        //   // Console.WriteLine(deserialize); 
        //}
    }