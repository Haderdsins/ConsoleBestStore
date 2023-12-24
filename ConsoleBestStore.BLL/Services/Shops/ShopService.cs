using ConsoleBestStore.BLL.Models.Create;
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
}