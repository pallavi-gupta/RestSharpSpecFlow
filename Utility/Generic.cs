using System.Diagnostics;
using GraphQL.NewtonsoftJson;
using Newtonsoft.Json.Linq;
using RestSharp;
using Newtonsoft.Json;

namespace ResSharpSpecFlow.Utility;

public class Generic
{
    private RestRequest req;
    private readonly ScenarioContext _scenarioContext;
    private RestClient _restClient;
    
    public Generic(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _restClient = _scenarioContext.Get<RestClient>("RestClient");
    }
    public  string getpropertyvalue(string key)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        
        string path = Path.Combine(currentDirectory.Split("bin")[0], "EndPoints.json");

        JObject jObject = JObject.Parse(File.ReadAllText(path));
        return (jObject[key].ToString());
    }

    public  RestRequest CreateRequest(string endpoint , string RequestType)
    {
        switch(RequestType)
        {
            case "POST": CreatePostReq(endpoint);
             break;
            
             case "GET": CreateGetReq(endpoint);
             break;
             
             case "PUT" : CreatePutReq(endpoint);
             break;
             
            case "DELETE" : CreateDelReq(endpoint);
                break;
            
            default: Console.WriteLine("Incorrect Reuqest Type ");
                break;
        }
     
        return req;

    }


    private void  CreatePostReq(string endpoint)
    {
        req=  new RestRequest(endpoint, Method.Post);
    }
    
    private void CreateGetReq(string endpoint)
    {
        req =  new RestRequest(endpoint, Method.Get);
    }
    
    private void CreatePutReq(string endpoint )
    {
        req = new RestRequest(endpoint, Method.Put);
    }
    
    private static RestRequest CreateDelReq(string endpoint )
    {
        return new RestRequest(endpoint, Method.Delete);
    }
    
 
    public RestResponse<TResponse> RExecuteRequest<TRequest, TResponse>(RestRequest request)
        where TRequest : class
        where TResponse : new()
    {
        // Execute the request and return the response
        RestResponse<TResponse> response = _restClient.Execute<TResponse>(request);
        
        return response;
    }
    
    public RestResponse RExecuteRequest(RestRequest request)
       
    {
        // Execute the request and return the response
        RestResponse response = _restClient.Execute(request);
        
        return response;
    }
    public List<TResponse> ListExecuteRequest<TRequest, TResponse>(RestRequest request)
        where TRequest : class
        where TResponse : new()
    {
        // Execute the request and return the response
        RestResponse<TResponse> response = _restClient.Execute<TResponse>(request);
        List<TResponse> items = JsonConvert.DeserializeObject<List<TResponse>>(response.Content);
        
        return items;
    }

    public object GetValue(object ob)
    {
        return ob.GetValue();
    }

    public  T MapJsonToModel<T>(object ob)
    {
        string jsonString = JsonConvert.SerializeObject(ob);
        // Deserialize JSON string to the specified model class
        T result = JsonConvert.DeserializeObject<T>(jsonString);
        return result;
    }
  
}