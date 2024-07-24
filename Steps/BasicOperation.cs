using System.Net;
using FluentAssertions;
using GraphQLProductApp.Controllers;
using GraphQLProductApp.Data;
using Newtonsoft.Json.Linq;
using ResSharpSpecFlow.Models.Request;
using ResSharpSpecFlow.Models.Response;
using RestSharp;
using TechTalk.SpecFlow.Assist;
using ResSharpSpecFlow.Utility;
using Xunit;


namespace RestSharpSpecflow.Steps;

[Binding]
public sealed class BasicOperation
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;

    private RestClient _restClient;
    private GetProduct? _getProductResponse;
    private CreateCart? _CreateCartResponse;
    private RestResponse<AddItemResponse> _AddItemResponse;
    private RestResponse<CreateCart> _CreateCartResponse1;
    

    private Product? _response;
    private Dictionary<string, string> _queryParameters;
    public static string _cartid;
    private Generic Gen;
    public  RestRequest request;
    
    public BasicOperation(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _restClient = _scenarioContext.Get<RestClient>("RestClient");
        Gen = new Generic(_scenarioContext);
    }


 /*   [Given(@"I perform a GET operation of ""(.*)""")]
    public async Task GivenIPerformAgetOperationOf(string path, Table table)
    {
        dynamic data = table.CreateDynamicInstance();
        var token = GetToken();
       // var token = Generic.GetToken();

        //Rest Request
        var request = new RestRequest(path);
        request.AddUrlSegment("id", (int)data.ProductId);
        request.AddHeader("Authorization", $"Bearer {token}");
        //Perform GET operation
        _response= await _restClient.GetAsync<Product>(request);
    }
*/
 
 
 [Given(@"I perform a GET operation of ""(.*)""")]
 public async Task GivenIPerformAgetOperationOf(string endpoint, Table table)
 {
     dynamic data = table.CreateDynamicInstance();
     var path = Gen.getpropertyvalue(endpoint);
     
     //Rest Request
     var request = new RestRequest(path);
     request.AddUrlSegment("id", (int)data.ProductId);
     //Perform GET operation
     _getProductResponse= await _restClient.GetAsync<GetProduct>(request);
 }

 [Given(@"I should get the details of product id ""(.*)""")]
 public void GivenIShouldGetTheDetailsOfProductId(string p0)
 {
     _getProductResponse.id.Should().Be(int.Parse(p0));
     
     // ScenarioContext.StepIsPending();
 }
 
 
 
    [Given(@"I should get the product name as ""(.*)""")]
    public void GivenIShouldGetTheProductNameAs(string value)
    {
        _response.Name.Should().Be(value);
    }


    private string GetToken()
    {
        //Rest Request
        var authRequest = new RestRequest("api/Authenticate/Login");
        
        //Typed object being passed as body in request
        authRequest.AddJsonBody(new LoginModel
        {
            UserName = "KK",
            Password = "123456"
        });

        //Perform GET operation
        var authResponse = _restClient.PostAsync(authRequest).Result.Content;

        //Token from JSON object
        return JObject.Parse(authResponse)["token"].ToString();
    }

    [Given(@"I perform a POST operation of ""(.*)""")]
    public async void GivenIPerformApostOperationOf(string p0, Table table)
    {
        dynamic data = table.CreateDynamicInstance();
        var token = GetToken();

        //Rest Request
        var request = new RestRequest(p0);
        request.AddJsonBody(new Product
        {
            Name = data.Name,
            Description = data.Description,
            Price = int.Parse(data.Price),
            ProductType = data.ProductType
        });
        
        request.AddHeader("Authorization", $"Bearer {token}");
        //Perform GET operation
        _response= await _restClient.PostAsync<Product>(request);

    }

    [Given(@"I Price of product should be ""(.*)""")]
    public void GivenIPriceOfProductShouldBe(string p0)
    {
        int exp = int.Parse(p0);
        _response.Price.Should().Be(exp);
      //  ScenarioContext.StepIsPending();
    }

    [Given(@"I perform a GET operation of ""(.*)"" with Query Parameter")]
    public async void GivenIPerformAgetOperationOfWithQueryParameter(string path, Table table)
    {
        var token = GetToken();
        // var token = Generic.GetToken();

        //Rest Request
        var request = new RestRequest(path);
        request.AddHeader("Authorization", $"Bearer {token}");
        
        foreach (var row in table.Rows)
        {
            request.AddParameter(row["Parameter"], row["Value"]);
        }
        
        _response= await _restClient.GetAsync<Product>(request);
        
        
      /*  dynamic data = table.CreateDynamicInstance();
        var token = GetToken();
        // var token = Generic.GetToken();

        //Rest Request
        var request = new RestRequest(path)
            .AddUrlSegment("id", (int)data.ID);
            
            request.AddUrlSegment("name" , data.Name);
//            .AddUrlSegment("name", data.Name);
            
        request.AddHeader("Authorization", $"Bearer {token}");
        //Perform GET operation
        _response= await _restClient.GetAsync<Product>(request);
*/
    }

    [Given(@"I should get the price should be ""(.*)""")]
    public void GivenIShouldGetThePriceShouldBe(string p0)
    {
        _response.Price.Should().Be(Int32.Parse(p0));
        //ScenarioContext.StepIsPending();
    }

 
    
    [Given(@"I hit the ""(.*)"" endpoint")]
    public  void GivenIHitTheEndpoint(string endpoint)
    {
//        dynamic data = table.CreateDynamicInstance();
        var path =  Gen.getpropertyvalue(endpoint);
      
        //Rest Request
        var request = new RestRequest(path);

        _CreateCartResponse=  _restClient.Post<CreateCart>(request);


    }

    [Then(@"Verify User should get the (.*) response code")]
    public void ThenVarifyUserShouldGetTheResponseCode(int p0)
    {
//        Assert.Equal(System.Net.HttpStatusCode.OK, _CreateCartResponse.StatusCode);
        _CreateCartResponse.Should().NotBeNull();
        //   _CreateCartResponse
        // ScenarioContext.StepIsPending();
    }

    [Then(@"CardID should not be Blank")]
    public void ThenCardIdShouldNotBeBlank()
    {
        _CreateCartResponse.cartId.Should().NotBeEmpty();
        _cartid = _CreateCartResponse.cartId;

        //   ScenarioContext.StepIsPending();
    }
    
    [Given(@"I send the POST request with the ""(.*)"" endpoint")]
    public  void GivenISendTheEndpoint(string endpoint)
    {
//        dynamic data = table.CreateDynamicInstance();
        var path =  Gen.getpropertyvalue(endpoint);
      
        //Rest Request
        var request = Gen.CreateRequest(path , "post");
       // var request = new RestRequest(path , Method.Post);
        request.AddUrlSegment("cartid", "toJr_47Ts86-PlbUjVK2l");

        request.AddJsonBody(new AddItem
        {
            productId = 4641,
            quantity = 2
        });

        _AddItemResponse  = _restClient.Execute<AddItemResponse>(request);
       // _CreateCartResponse=  _restClient.Post<CreateCart>(request);


    }


 

 

   

  
  
    
    [Then(@"Verify  Created should be true in CreateCart")]
    public void ThenVerifyCreatedShouldBeTruenCreateCart()
    {
        _CreateCartResponse.created.Should().Be(true);
        //  _CreateCartResponse1.Data.created.Should().Be(true);
        //   ScenarioContext.StepIsPending();
    }

    
 
    
    
}