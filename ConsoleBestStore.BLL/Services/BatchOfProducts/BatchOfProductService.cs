using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Get;
using ConsoleBestStore.BLL.Models.Search;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.DAL.DataBase;
using ConsoleBestStore.DAL.Models;

namespace ConsoleBestStore.BLL.Services.BatchOfProducts;

public class BatchOfProductService : IBatchOfProductService
{
    
    private readonly ShopDbContext _dbContext;
    public BatchOfProductService(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void CreateBatchOfProduct(CreateBatchOfProductModel model)
    {
        var omgitem = new OmgItem();
        omgitem.StoreId = model.StoreId;
        omgitem.ProductId = model.ProductId;
        omgitem.Count = model.Count;
        omgitem.Price = model.Price;
        
        // logic to add database
        _dbContext.OmgItems.Add(omgitem);
        _dbContext.SaveChanges();
        
    }
    
    public void Delete(int itemId)
    {
        var itemToDelete = _dbContext.OmgItems.Find(itemId);

        if (itemToDelete != null)
        {
            _dbContext.OmgItems.Remove(itemToDelete);
            _dbContext.SaveChanges();
        }
        else
        {
            // Обработка случая, когда магазин с указанным ID не найден
            throw new ArgumentException("Item not found.");
        }
    }
    public void Update(int itemId, UpdateBatchOfProductModel model)
    {
        var itemToUpdate = _dbContext.OmgItems.Find(itemId);

        if (itemToUpdate != null)
        {
            // Обновление атрибутов продукта
            itemToUpdate.ProductId = model.ProductId; 
            itemToUpdate.StoreId = model.StoreId;
            itemToUpdate.Count += model.Count;
            itemToUpdate.Price = model.Price;
            _dbContext.SaveChanges();
        }
        else
        {
            // Обработка случая, когда продукт с указанным ID не найден
            throw new ArgumentException("Item not found.");
        }
    }
    
    public Shop FoundStoreWhereMinPriceProduct(int productId)
    {
        var cheapestItem = _dbContext.OmgItems
            .Where(item => item.ProductId == productId)
            .OrderBy(item => item.Price)
            .FirstOrDefault();

        if (cheapestItem != null)
        {
            var store = _dbContext.Shops.Find(cheapestItem.StoreId);
            return store;
        }

        // Handle the case where the product is not found or no store has the product
        throw new Exception("Product not found or no store has the product.");
    }
//TODO: добавить в получаемые переменнные productId, имзенить принцип расчета quantity, проверку на ноли и округление
    public GetItemsForAmountModel GetItemsForAmount(decimal amount)
    {
        var affordableItems = _dbContext.OmgItems
            .Where(item => item.Price <= amount)
            .OrderBy(item => item.Price)
            .ToList();

        var result = new List<AffordableItemModel>();

        // Рассчитываем, сколько товаров можно купить для заданной суммы
        foreach (var item in affordableItems)
        {
            // Проверка на ноль
            if (item.Price != 0)
            {
                int quantity = (int)Math.Floor(amount / item.Price); // Изменение принципа расчета
                result.Add(new AffordableItemModel { Item = item, Quantity = quantity });
            }
        }

        return new GetItemsForAmountModel(result);
    }
    
    public GetAllStoresModel FindCheapestStoreForBatches(List<CheapestStoreModel> model)
    {
        var array = new List<int>(); // хранятся StoreIds

        for (var i = 0; i < model.Count; i++)
        {
            var storeIds = _dbContext.OmgItems
                .Where(x => 
                    x.ProductId == model[i].ProductId && 
                    x.Count >= model[i].BatchCount)
                .Select(x => x.StoreId)
                .ToList();

            if (i == 0)
            {
                array = storeIds;
            }
            else
            {
                array = array.Intersect(storeIds).ToList();
            }
        }
        // array - 5 6, это ids
        var minTotalPrice = decimal.MaxValue;
        
        var cheapestStoreId = 0;

        foreach (var storeId in array)
        {
            // 5
            var allStoreProducts = _dbContext.OmgItems
                .Where(x => x.StoreId == storeId)
                .ToList();

            // ручка, тетрадь, бумага
            decimal totalBatchPrice = 0;

            // items - ручка и тетрадь + их кол-во
            foreach (var item in model)
            {
                // тетрадь
                var product = allStoreProducts
                    .FirstOrDefault(x => x.ProductId == item.ProductId);
                
                totalBatchPrice += product!.Price * item.BatchCount;
            }

            if (totalBatchPrice < minTotalPrice)
            {
                minTotalPrice = totalBatchPrice;
                cheapestStoreId = storeId;
            }
        }

        if (cheapestStoreId == 0)
        {
            throw new ArgumentException("Error!");
        }

        var cheapestStore = _dbContext.Shops
            .FirstOrDefault(x => x.Id == cheapestStoreId);
        return new GetAllStoresModel
        {
            Id=cheapestStore.Id,
            Name = cheapestStore.Name,
            Address = cheapestStore.Address,
        };
    }



    
    public decimal PurchaseItems(Dictionary<int, int> itemQuantities)
    {
        decimal totalCost = 0;

        foreach (var itemQuantity in itemQuantities)
        {
            int itemId = itemQuantity.Key;
            int quantityToBuy = itemQuantity.Value;

            var item = _dbContext.OmgItems.FirstOrDefault(i => i.Id == itemId && i.Count >= quantityToBuy);

            if (item != null)
            {
                item.Count -= quantityToBuy;
                totalCost += item.Price * quantityToBuy;
            }
            else
            {

                throw new InvalidOperationException($"На складах недостаточно товара с id: {itemId}");
            }
        }

        _dbContext.SaveChanges();

        return totalCost;
    }
    
}