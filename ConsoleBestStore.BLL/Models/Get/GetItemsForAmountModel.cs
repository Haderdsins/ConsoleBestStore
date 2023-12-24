namespace ConsoleBestStore.BLL.Models.Get;

public class GetItemsForAmountModel
{
    public List<AffordableItemModel> Items { get; set; }
    //public int TotalQuantity { get; set; }

    public GetItemsForAmountModel(List<AffordableItemModel> items)
    {
        Items = items;
        //TotalQuantity = totalQuantity;
    }
}