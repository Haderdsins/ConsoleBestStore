using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.DAL.DataBase;
using ConsoleBestStore.DAL.Models;

namespace ConsoleBestStore.BLL.Services.Shops;

public class ShopService : IShopService
{
    private readonly ShopDbContext _dbContext;

    public ShopService(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Create(CreateShopModel model)
    {
        var shop = new Shop();
        shop.Name = model.Name;
        shop.Address = model.Address;

        // logic to add database
        _dbContext.Shops.Add(shop);
        _dbContext.SaveChanges();
    }
    public void Delete(int StoreId)
    {
        var shopToDelete = _dbContext.Shops.Find(StoreId);

        if (shopToDelete != null)
        {
            _dbContext.Shops.Remove(shopToDelete);
            _dbContext.SaveChanges();
        }
        else
        {
            // Обработка случая, когда магазин с указанным ID не найден
            throw new ArgumentException("Store not found.");
        }
    }
    public void Update(int storeId, UpdateShopModel model)
    {
        var storeToUpdate = _dbContext.Shops.Find(storeId);

        if (storeToUpdate != null)
        {
            // Обновление атрибутов продукта
            storeToUpdate.Name = model.Name; // Пример, добавьте другие атрибуты, которые необходимо обновить
            storeToUpdate.Address = model.Address;

            _dbContext.SaveChanges();
        }
        else
        {
            // Обработка случая, когда продукт с указанным ID не найден
            throw new ArgumentException("Store not found.");
        }
    }
}