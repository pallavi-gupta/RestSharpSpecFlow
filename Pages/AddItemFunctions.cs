using RestSharp;
using ResSharpSpecFlow.Utility;

namespace ResSharpSpecFlow.Pages;

public class AddItemFunctions
{
    public  static RestRequest request;
    private readonly ScenarioContext _scenarioContext;
    private RestClient _restClient;
    private Generic Gen;

    public AddItemFunctions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _restClient = _scenarioContext.Get<RestClient>("RestClient");
         Gen = new Generic(_scenarioContext);
    }
    
    public RestRequest CreateRequestWithTheEndpoint(string Requesttype, string endpoint)
    {
        var path =  Gen.getpropertyvalue(endpoint);
        request = Gen.CreateRequest(path,Requesttype );
        return request;
    }
    
    public RestRequest AddItemQueryParameterAndJsonBody(string cart , Object testdata)
    {  
        request.AddUrlSegment("cartid", cart);
        request.AddJsonBody(testdata);
        return request;
    }
    
    public RestRequest AddItemQueryParameter(string key , string value)
    {  
        request.AddUrlSegment(key, value);
        return request;
    }
    
    public RestRequest AddItemJsonBody(Object testdata)
    {  
        request.AddJsonBody(testdata);
        return request;
    }
}