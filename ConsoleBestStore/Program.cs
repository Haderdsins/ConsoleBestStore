using ConsoleBestStore.BLL.Services.Shops;
using ConsoleBestStore.DAL.DataBase;
using ConsoleBestStore.UI.Visions;

class Program
{
    static void Main()
    {

        var dbContext = new ShopDbContext(); 
        IShopService shopService = new ShopService(dbContext); 
        
        var shopManager = new ShopManager(shopService);

        // Вызываем метод Run для взаимодействия с пользователем
        shopManager.Run();
    }
}