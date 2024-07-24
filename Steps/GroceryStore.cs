using System.Net;
using RestSharp;
using ResSharpSpecFlow.Utility;
using ResSharpSpecFlow.Models.Request;
using ResSharpSpecFlow.Models.Response;
using ResSharpSpecFlow.JsonBody;
using FluentAssertions;
using ResSharpSpecFlow.Pages;

namespace RestSharpSpecflow.Steps;

[Binding]
public class GroceryStore
{
    private readonly ScenarioContext _scenarioContext;
    private RestClient _restClient;
    private Generic Gen;
    private AddItemFunctions AdditemFun;
    public  static RestRequest request;
    private AddItemJson AI;
    private RestResponse<GetItemInCart> getItemInCartResponse;
    private RestResponse<AddItemResponse> AddItemResponse;
    private RestResponse<CreateCart> _CreateCartResponse;
    private RestResponse putresponse;
    private RestResponse delresponse;
    
//    private CreateCart? _CreateCartResponse;
    public static string _cartid; 
    public static string _productid; 
    public static int _itemid;
    public static int requestProductid;
   // public static int itemID;
    

    public GroceryStore(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _restClient = _scenarioContext.Get<RestClient>("RestClient");
         Gen = new Generic(_scenarioContext);
         AdditemFun = new AddItemFunctions(_scenarioContext);
         AI = new AddItemJson();
    }

    [When(@"I send the ""(.*)"" request with the ""(.*)"" endpoint")]
    [Given(@"I send the ""(.*)"" request with the ""(.*)"" endpoint")]
    public void GivenISendTheRequestWithTheEndpoint(string Requesttype, string endpoint)
    {
        request = AdditemFun.CreateRequestWithTheEndpoint(Requesttype,endpoint);
    }
    
    [When(@"Execute the Create Cart request")]
    public void WhenExecuteCreateCart()
    {
        _CreateCartResponse  =  Gen.RExecuteRequest<object, CreateCart>(request);
    }
    
    [Then(@"Verify Created should be true in CreateCart")]
    public void ThenVerifyCreatedShouldBeTruenCreateCart()
    {
        _CreateCartResponse.Data.created.Should().Be(true);
    }
    
    [Then(@"Verify CartID should not be Blank New")]
    public void ThenVerifyCartIDIdShouldNotBeBlankNew()
    {
        _CreateCartResponse.Data.cartId.Should().NotBe(null);
        _cartid = _CreateCartResponse.Data.cartId;
    }
    
    [When(@"With the Cart valid JsonBody")]
    [Given(@"With the Cart valid JsonBody")]
    public void GivenWithTheValidJsonBody()
    {
        object result = Gen.GetValue(AI.AddItem());
        request = AdditemFun.AddItemJsonBody(result);
     
      AddItem deserializedModel = Gen.MapJsonToModel<AddItem>(result);

      requestProductid = deserializedModel.productId;
      
      //Console.WriteLine(deserializedModel.productId);
      //Console.WriteLine(deserializedModel.quantity);
      

      //request.Parameters.GetParameters(JsonParameter).GetValue();
      //Console.WriteLine(request.Parameters.GetParameters().p);
    }

    [Then(@"Verify Created should be true in AddItemInCart")]
    public void ThenVerifyCreatedShouldBeTruenAddItemInCart()
    {
        AddItemResponse.Data.created.Should().Be(true);
    }
    
    
    [Then(@"Verify ItemID should not be Blank")]
    public void ThenVerifyItemIdShouldNotBeBlank()
    {
        AddItemResponse.Data.itemId.Should().NotBe(null);
    }

    [When(@"With the Cart Query Parameter")]
    [Given(@"With the Cart Query Parameter")]
    public void GivenWithTheQueryParameter()
    {
        AddItemJson AI = new AddItemJson();
        request =AdditemFun.AddItemQueryParameter("cartid",_cartid);
    }
    
    [When(@"With the Item Query Parameter")]
    public void AddItemIDQueryParameter()
    {
        AddItemJson AI = new AddItemJson();
        request =AdditemFun.AddItemQueryParameter("itemid",_itemid.ToString());
    }
    
    [When(@"With the None Request Body")]
    public void NoneRequestBody()
    {
       // AddItemJson AI = new AddItemJson();
        request =request.AddParameter("application/json", "None", ParameterType.RequestBody);
    }
    
    [Given(@"With the Cart Invalid JsonBody")]
    public void GivenWithTheInvalidJsonBody()
    {
        request =AdditemFun.AddItemJsonBody(AI.InvalidAddItem());
    }
    
    [Given(@"Execute the Add Item request with all the parameters")]
    [When(@"Execute the Add Item request with all the parameters")]
    public void WhenExecuteTheAddItemRequest()
    {
        AddItemResponse =  Gen.RExecuteRequest<object, AddItemResponse>(request);
        _itemid = AddItemResponse.Data.itemId;
          
    }
    
    [When(@"Execute the Get Item request with all the parameters")]
    public void WhenExecuteTheGetItemRequest()
    {
        getItemInCartResponse =  Gen.RExecuteRequest<object, GetItemInCart>(request);
    }
    
    [Then(@"Verify the ""(.*)"" Status Code of Add Item")]
    public void ThenVerifyTheAddItemStatusCode(string statuscode)
    {
      //  AddItemResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        if (statuscode.Equals("200"))
        {
            AddItemResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        else if (statuscode.Equals("400"))
        {
            AddItemResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        else if (statuscode.Equals("201"))
        {
            AddItemResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            
        }
        
    }

    [Then(@"Verify the ""(.*)"" Status Code for Get Item")]
    public void ThenVerifyTheGetItemStatusCode(string statuscode)
    {
        if (statuscode.Equals("200"))
        {
            getItemInCartResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
       else if (statuscode.Equals("400"))
        {
            getItemInCartResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
       else if (statuscode.Equals("201"))
       {
           getItemInCartResponse.StatusCode.Should().Be(HttpStatusCode.Created);
       }
    }

    [Then(@"Verify the Productid should match with the POST request productid")]
    public void ThenVerifyTheProductIDInResponse()
    {
        List<GetItemInCart> items = Gen.ListExecuteRequest<object, GetItemInCart>(request);
        GetItemInCart item = items[0];
        
        item.ProductId.Should().Be(requestProductid);
        item.Id.Should().NotBe(null);
        item.ProductId.Should().NotBe(null);
        item.Quantity.Should().NotBe(null);

    }
    
    [When(@"Execute the Put Item request with all the parameters")]
    public void WhenExecuteThePutItemRequest()
    {
        putresponse =  Gen.RExecuteRequest(request);
    }
    
    
    [Then(@"Verify the ""(.*)"" Status Code for Update Item")]
    public void ThenVerifyThePutItemStatusCode(string statuscode)
    {
        if (statuscode.Equals("200"))
        {
            putresponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        else if (statuscode.Equals("400"))
        {
            putresponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        else if (statuscode.Equals("201"))
        {
            putresponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        else if (statuscode.Equals("204"))
        {
            putresponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
    
    [When(@"Execute the Del Item request with all the parameters")]
    public void WhenExecuteTheDeltItemRequest()
    {
        delresponse =  Gen.RExecuteRequest(request);
    }
    
    
    [Then(@"Verify the ""(.*)"" Status Code for Del Item")]
    public void ThenVerifyTheDelItemStatusCode(string statuscode)
    {
        if (statuscode.Equals("200"))
        {
            delresponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        else if (statuscode.Equals("400"))
        {
            delresponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        else if (statuscode.Equals("201"))
        {
            delresponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        else if (statuscode.Equals("204"))
        {
            delresponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }

    
    
    [Then(@"Verify the Productid should not be their")]
    public void ThenVerifyTheProductIDInGetResponse()
    {
   //     getItemInCartResponse.Data.Should().Be(null);
    }
    
    
}