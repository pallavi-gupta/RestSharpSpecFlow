using Newtonsoft.Json;
using ResSharpSpecFlow.Models.Request;
using RestSharp;
using ResSharpSpecFlow.Models;
namespace ResSharpSpecFlow;

public class Demo
{
    private Helper helper;

    public Demo()
    {
        helper = new Helper();
    }
   /* public async Task<RestResponse> GetUsers(string baseUrl)
    {
        //helper.AddCertificate("", "");
        var client = helper.SetUrl(baseUrl, "api/users?page=2");
        var request = helper.CreateGetRequest();
        request.RequestFormat = DataFormat.Json;
        var response = await helper.GetResponseAsync(client, request);
        return response;
    }
*/
    public async Task<RestResponse> CreateNewUser<T>(string baseUrl, dynamic payload)
    {
        var client = helper.SetUrl(baseUrl, "api/users");
        //var jsonString = HandleContent.SerializeJsonString(payload);
        var request = helper.CreatePostRequest<CreateUserReq>(payload);
        var response = await helper.GetResponseAsync<T>(client, request);
        return response;
    }
}