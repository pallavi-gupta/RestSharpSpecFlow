using ResSharpSpecFlow.Models.Request;
using ResSharpSpecFlow.Models.Response;

namespace ResSharpSpecFlow.JsonBody;

public class AddItemJson
{
    public object AddItem()
    {
        return new AddItem
        {
            productId = SelectproductID(),
            quantity = 2
        };
    }

    public object InvalidAddItem()
    {
        return new AddItem
        {
            productId = 0,
            quantity = 2
        };
    }

    private int SelectproductID()
    {
        List<int> productIDs = new List<int> { 4641, 4643, 4646, 1225, 3674,2585 };
        int count = productIDs.ToList().Count;
        Random random = new Random();
        int index = random.Next(count);
        return productIDs[index];
    }
}