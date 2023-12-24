using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Delete;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.BLL.Services.Shops;

namespace ConsoleBestStore.UI.Visions;

public class ShopManager
{
    private IShopService _storeService;

    public ShopManager(IShopService storeService)
    {
        _storeService = storeService;
    }

    public void Run()
    {
        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Создать магазин");
        Console.WriteLine("2. Удалить магазин");
        Console.WriteLine("3. Обновить магазин");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                CreateShop();
                break;
            case 2:
                DeleteShop();
                break;
            case 3:
                UpdateShop();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }

    public void CreateShop()
    {
        Console.WriteLine("Введите название магазина:");
        string name = Console.ReadLine();
        Console.WriteLine("Введите адрес магазина:");
        string address = Console.ReadLine();

        var createStoreModel = new CreateShopModel() { Name = name, Address = address };
        _storeService.Create(createStoreModel);

        Console.WriteLine("Магазин создан успешно!");
    }
    public void DeleteShop()
    {
        Console.WriteLine("Введите идентификатор магазина для удаления:");
        int storeId = int.Parse(Console.ReadLine());
        
        var deleteStoreModel = new DeleteShopModel() { StoreId = storeId };
        _storeService.Delete(deleteStoreModel.StoreId);

        Console.WriteLine("Магазин удален успешно!");
    }

    public void UpdateShop()
    {
        Console.WriteLine("Введите идентификатор магазина для обновления:");
        int storeId = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите новое название магазина:");
        string newName = Console.ReadLine();
        Console.WriteLine("Введите новый адрес магазина:");
        string newAddress = Console.ReadLine();

        var updateStoreModel = new UpdateShopModel() { StoreId = storeId, Name = newName, Address = newAddress };
        _storeService.Update(updateStoreModel.StoreId, updateStoreModel);

        Console.WriteLine("Магазин обновлен успешно!");
        
    }

}