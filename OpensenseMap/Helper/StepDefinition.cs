using MySqlConnector;
using Newtonsoft.Json;
using OpensenseMap.Models;
using RestSharp;
using System.Configuration;
using System.Net;
using TechTalk.SpecFlow;

namespace OpensenseMap.StepDefinition
{
    [Binding]
    internal class StepDefinition
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private Task<RestResponse> responseTask;
        private HttpStatusCode statusCode;
        private MySqlConnection sqlConnection;

        [BeforeScenario]
        public void BeforeScenario()
        {

        }


        public void AddParametersToScenarioContext(string parameters)
        {
            object[] obj = parameters.Split(',').ToArray();
            ScenarioContext.Current.Add("jsonParams", obj);
        }

        [Given(@"Setup a new connection to endoint '(.*)'")]
        public void SetupNewConnectionToEndpoint(string url)
        {
            client = new RestClient(url);
        }

        [Given(@"Setup new (POST|GET) request to endpint '(.*)'")]
        public void SetupRequestEndpint(string method, string endpointName)
        {            
            Method methodType = Enum.Parse<Method>(CapitalFirstLetter(method));
            request = new RestRequest(endpointName, methodType);

            if(methodType == Method.Post)
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", ConfigurationManager.AppSettings["Bearer"]);
            }           
        }

        [Given(@"Open a json file '(.*)' and prepare with parameters '(.*)'")]
        public void OpenJsonAndFillParams(string jsonNmae, string parameters)
        {
            object[] obj = parameters.Split(';').ToArray();
            string jsonPrepared = string.Format(new StreamReader($"Requests/{jsonNmae}").ReadToEnd()
            , obj);
            request.AddParameter("application/json", jsonPrepared, ParameterType.RequestBody);
        }

        [Given(@"Send prepared (POST|GET) requests? (|async|)")]
        public void GivenSendPreparedRequests(string method, string asyn)
        {
            Method methodType = Enum.Parse<Method>(CapitalFirstLetter(method));
            bool async = asyn == "" ? false : true;

            try
            {
                switch (methodType)
                {
                    case Method.Post:
                        if (async)
                        {
                            responseTask = client.PostAsync(request);
                            responseTask.Wait();
                        }
                        else
                        {
                            response = client.Post(request);
                        }
                        break;
                    case Method.Get:
                        if (async)
                        {
                            responseTask = client.GetAsync(request);
                            responseTask.Wait();
                        }
                        else
                        {
                            response = client.Get(request);
                        }
                        break;
                }
                if (async)
                {
                    response = responseTask.Result;
                    this.statusCode = responseTask.Result.StatusCode;
                }
                else
                {
                    // response = client.Get(request);
                    this.statusCode = response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                this.statusCode = Enum.Parse<HttpStatusCode>(ex.InnerException.Message.Split(' ').Last());
            }
        }

        [Then(@"Response status is '(OK|BadRequest|NotFound|BadGateway|Forbidden|UnprocessableEntity)'")]
        public void ThenResponseStatusIs(string statusCode)
        {
            try
            {
                bool resalt = this.statusCode == Enum.Parse<HttpStatusCode>(statusCode);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        [Given(@"Connect to database")]
        public void GivenConnectToDatabase()
        {

            string? connectionString = ConfigurationManager.AppSettings["ConnectionStringDB"];
            this.sqlConnection = new MySqlConnection(connectionString);
        }

        [Given(@"Insert to database first row of retured values")]
        public void GivenInsertToDatabaseFirstRowOfReturedValues()
        {
             var boxesData = JsonConvert.DeserializeObject<List<BoxesDataMeasurementsReturn>>(response.Content.ToString());
            var firstBox = boxesData.First();
            MySqlCommand insert = new MySqlCommand();
            insert.CommandText = $"INSERT INTO boxesdata (BoxId, Phenomenon, CreatedAt, InsertTime, SensorId, Value) " +
                $"VALUES  ('{firstBox.boxId}', '{firstBox.phenomenon}','{firstBox.createdAt.GetDateTimeFormats()[21]}','{DateTime.Now.GetDateTimeFormats()[21]}','{firstBox.sensorId}','{firstBox.value}')";
            try
            {
                OpenDbConnection(sqlConnection);
                insert.Connection = sqlConnection;
                insert.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            
        }

        [Given(@"Select first row")]
        public void GivenSelectFirstRow()
        {
            MySqlCommand select = new MySqlCommand();
            select.CommandText = $"SELECT * FROM boxesdata ORDER BY Id DESC LIMIT 1;";
            try
            {
                OpenDbConnection(sqlConnection);
                select.Connection = sqlConnection;
                select.ExecuteScalar();

            }
            catch (Exception ex)
            {
            }
        }

        [Given(@"Prepare get url using '(.*)'")]
        public void GivenPrepareGetUrlUsingAnd(string parameters)
        {
            request.AddUrlSegment("0", parameters);

            //this.request

        }

        [Given(@"Send get requests? (|async|)")]
        public void GivenSendGetRequests(string asyn)
        {
            if (asyn == "async")
            {
                try
                {
                    responseTask = client.GetAsync(request);
                    responseTask.Wait();
                    this.statusCode = responseTask.Result.StatusCode;
                    response = responseTask.Result;
                }
                catch (Exception ex)
                {
                    this.statusCode = Enum.Parse<HttpStatusCode>(ex.InnerException.Message.Split(' ').Last());
                }

            }
            else
            {
                response = client.Get(request);
                this.statusCode = response.StatusCode;
            }
        }
        private void OpenDbConnection(MySqlConnection sqlConnection)
        {
            if (sqlConnection.State != System.Data.ConnectionState.Open )
            {
                this.sqlConnection.Open();
            }
        }

        private string CapitalFirstLetter(string capital) 
        {
            return $"{capital[0].ToString().ToUpper()}{capital.Substring(1).ToLower()}";
        }
    }
}
