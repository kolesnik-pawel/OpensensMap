using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OpensenseMap.StepDefinition
{
    [Binding]
    internal class StepDefinition
    {
        private Connection connect;
        private RestRequest request;

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
        public void SetupNewBonnectionToEndpoint(string url)
        {
            connect = new Connection(url);

            
        }

        [Given(@"Setup new request to endpint '(.*)'")]
        public void SetupRequestEndpint(string endpointName) 
        {
            request = new RestRequest(endpointName);
            request.AddParameter("bearer", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE0ODMwMDYxNTIsImV4cCI6MTQ4MzAwOTc1MiwiaXNzIjoibG9jYWxob3N0OjgwMDAiLCJzdWIiOiJ0ZXN0QHRlc3QuZGUiLCJqdGkiOiJmMjNiOThkNi1mMjRlLTRjOTQtYWE5Ni1kMWI4M2MzNmY1MjAifQ.QegeWHWetw19vfgOvkTCsBfaSOPnjakhzzRjVtNi-2Q");
        }

        [Given(@"Open a json file '(.*)' and prepare with parameters '(.*)'")]
        public void OpenJsonAndFillParams(string jsonNmae, string parameters)
        {
            object[] obj = parameters.Split(',').ToArray(); 
            string s = string.Format(new StreamReader($"Requests/{jsonNmae}").ReadToEnd()
            , obj);
           
            request.AddStringBody(s, "application/json");
        }

    }
}
