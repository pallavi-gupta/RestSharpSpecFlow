using System;
using ResSharpSpecFlow.Utility;
using RestSharp;

namespace RestSharpSpecflow.Drivers
{
    public class Driver
    {
        public Driver(ScenarioContext scenarioContext)
        {
            var restClientOptions = new RestClientOptions
            {
                
         //       BaseUrl = new Uri("https://localhost:5001/"),
                 BaseUrl = new Uri("http://simple-grocery-store-api.online"),
                RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
            };

            //Rest Client
            var restClient = new RestClient(restClientOptions);
            
            //Add into ScenarioContext
            scenarioContext.Add("RestClient", restClient);
        }
    }
}

// Azure-ci/cd
// Wagering - betting Gaming