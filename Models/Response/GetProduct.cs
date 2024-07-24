using Newtonsoft.Json;

namespace ResSharpSpecFlow.Models.Response;

public class GetProduct
{
    public int id { get; set; }
    public string category { get; set; }
    public string name { get; set; }
    public string manufacturer { get; set; }
    public double price { get; set; }

    [JsonProperty("current-stock")]
    public int currentstock { get; set; }
    public bool inStock { get; set; }
}