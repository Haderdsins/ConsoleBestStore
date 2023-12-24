using ConsoleBestStore.BLL.Services.BatchOfProducts;
using ConsoleBestStore.BLL.Services.Products;
using ConsoleBestStore.BLL.Services.Shops;
using ConsoleBestStore.DAL.DataBase;
using ConsoleBestStore.UI.Managers;
using ConsoleBestStore.UI.Visions;

class Program
{
    public static void Main()
    {
        var dbContext = new ShopDbContext();
        IShopService shopService = new ShopService(dbContext);
        IProductService productService = new ProductService(dbContext);
        IBatchOfProductService batchOfProductService = new BatchOfProductService(dbContext);
        Console.Clear();
        Console.WriteLine("С чем хотите сегодня работать?");
        Console.WriteLine("1. Магазины");
        Console.WriteLine("2. Продукты");
        Console.WriteLine("3. Позиция продукта в магазине");
        
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                var shopManager = new ShopManager(shopService);
                shopManager.Run();
                break;
            case 2:
                var productManager = new ProductManager(productService);
                productManager.RunProduct();
                break;
            case 3:
                var batchOfProductManager = new BatchOfProductManager(batchOfProductService);
                batchOfProductManager.RunBatchOfProduct();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }
}