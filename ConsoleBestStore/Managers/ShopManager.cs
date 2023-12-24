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
        Console.Clear();
        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Создать магазин");
        Console.WriteLine("2. Удалить магазин");
        Console.WriteLine("3. Обновить магазин");
        Console.WriteLine("4. Вывести все магазины");
        Console.WriteLine("5. Назад");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                CreateShop();
                Run();
                break;
            case 2:
                DeleteShop();
                Run();
                break;
            case 3:
                UpdateShop();
                Run();
                break;
            case 4:
                DisplayAllShops();
                Run();
                break;
            case 5:
                Program.Main();
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

    // Проверяем, существует ли магазин с указанным идентификатором
    var existingShop = _storeService.GetShopById(storeId);

    if (existingShop != null)
    {
        Console.WriteLine("Введите новое название магазина:");
        string newName = Console.ReadLine();
        Console.WriteLine("Введите новый адрес магазина:");
        string newAddress = Console.ReadLine();

        var updateStoreModel = new UpdateShopModel() { StoreId = storeId, Name = newName, Address = newAddress };
        _storeService.Update(updateStoreModel.StoreId, updateStoreModel);

        Console.WriteLine("Магазин обновлен успешно!");
    }
    else
    {
        Console.WriteLine($"Магазин с идентификатором {storeId} не найден.");
    }
}

    private void DisplayAllShops()
    {
        var shops = _storeService.GetAllShops();
    
        if (shops.Any())
        {
            Console.WriteLine("Список всех магазинов:");
            for (int i = 0; i < shops.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Магазин {shops[i].Name}, Адрес: {shops[i].Address}");
            }
        }
        else
        {
            Console.WriteLine("Нет доступных магазинов.");
        }
    }


}