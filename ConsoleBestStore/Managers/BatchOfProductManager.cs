using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Search;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.BLL.Services.BatchOfProducts;

namespace ConsoleBestStore.UI.Managers;

public class BatchOfProductManager
{
    private IBatchOfProductService _batchOfProductService;

    public BatchOfProductManager(IBatchOfProductService batchOfProductService)
    {
        _batchOfProductService = batchOfProductService;
    }
    public void RunBatchOfProduct()
    {
        Console.Clear();
        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Создать продукт");
        Console.WriteLine("2. Удалить продукт");
        Console.WriteLine("3. Завезти партию товаров в магазин");
        Console.WriteLine("4. Найти магазин, в котором определенный товар самый дешевый");
        Console.WriteLine("5. Понять, какие товары можно купить в магазине на некоторую сумму");
        Console.WriteLine("6. Купить партию товаров в магазине");
        Console.WriteLine("7. Найти, в каком магазине партия товаров");
        Console.WriteLine("8. Назад");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                CreateBatchOfProduct();
                RunBatchOfProduct();
                break;
            case 2:
                DeleteBatchOfProduct();
                RunBatchOfProduct();
                break;
            case 3:
                UpdateBatchOfProduct();
                RunBatchOfProduct();
                break;
            case 4:
                FoundStoreWhereMinPriceProduct();
                RunBatchOfProduct();
                break;
            case 5:
                GetItemsInPrice();
                RunBatchOfProduct();
                break;
            case 6:
                PurchaseBatchOfProduct();
                RunBatchOfProduct();
                break;
            case 7:
                FindCheapestStoreForProduct();
                RunBatchOfProduct();
                break;
            case 8:
                Program.Main();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
        
        
    }

    private void CreateBatchOfProduct()
    {
        Console.WriteLine("Введите идентификатор магазина:");
        int storeId = int.Parse(Console.ReadLine());

        // Проверка существования магазина по введенному идентификатору
        if (!_batchOfProductService.CheckStoreExistence(storeId))
        {
            Console.WriteLine("Ошибка: Магазин с указанным идентификатором не найден.");
            return;
        }

        Console.WriteLine("Введите идентификатор продукта:");
        int productId = int.Parse(Console.ReadLine());

        // Проверка существования продукта по введенному идентификатору
        if (!_batchOfProductService.CheckProductExistence(productId))
        {
            Console.WriteLine("Ошибка: Продукт с указанным идентификатором не найден.");
            return;
        }

        Console.WriteLine("Введите количество товара:");
        int count = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите цену товара:");
        int price = int.Parse(Console.ReadLine());

        var createBatchOfProductModel = new CreateBatchOfProductModel
        {
            StoreId = storeId,
            ProductId = productId,
            Count = count,
            Price = price
        };

        try
        {
            _batchOfProductService.CreateBatchOfProduct(createBatchOfProductModel);
            Console.WriteLine("Позиция продукта в магазине создана успешно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private void DeleteBatchOfProduct()
    {
        Console.WriteLine("Введите идентификатор позиции продукта в магазине для удаления:");
        int itemId = int.Parse(Console.ReadLine());

        try
        {
            _batchOfProductService.Delete(itemId);
            Console.WriteLine("Позиция продукта в магазине удалена успешно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private void UpdateBatchOfProduct()
    {
        Console.WriteLine("Введите идентификатор позиции продукта в магазине для новой поставки:");
        int itemId = int.Parse(Console.ReadLine());

        if (!_batchOfProductService.CheckItemExistence(itemId))
        {
            Console.WriteLine("Ошибка: Позиция продукта с указанным ID не найдена.");
            return;
        }

        Console.WriteLine("Введите количество привезенного товара:");
        int newCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите новую цену товара:");
        int newPrice = int.Parse(Console.ReadLine());

        try
        {
            var updateModel = new UpdateBatchOfProductModel
            {
                Count = newCount,
                Price = newPrice
            };
            _batchOfProductService.Update(itemId, updateModel);
            Console.WriteLine("Поставка продукта в магазине добавлена успешно!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    private void FoundStoreWhereMinPriceProduct()
    {
        Console.WriteLine("Введите идентификатор продукта:");
        int productId = int.Parse(Console.ReadLine());

        try
        {
            var cheapestStore = _batchOfProductService.FoundStoreWhereMinPriceProduct(productId);
            Console.WriteLine($"Самый дешевый магазин для продукта с ID {productId}: {cheapestStore.Name}, {cheapestStore.Address}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    private void GetItemsInPrice()
    {
        Console.WriteLine("Введите сумму для поиска товаров:");
        decimal amount = decimal.Parse(Console.ReadLine());

        var affordableItems = _batchOfProductService.GetItemsForAmount(amount);

        Console.WriteLine("Список доступных товаров:");
        foreach (var item in affordableItems.Items)
        {
            var product = _batchOfProductService.GetProductById(item.Item.ProductId);
            var store = _batchOfProductService.GetShopById(item.Item.StoreId);

            Console.WriteLine($"{item.Item.ProductId}. {product.Name} - {item.Quantity} шт. (Магазин: {store.Name}, Адрес: {store.Address})");
        }
    }

    public void PurchaseBatchOfProduct()
    {
        Console.WriteLine("Введите идентификатор позиции продукта в магазине для покупки:");
        int itemId = int.Parse(Console.ReadLine());

        if (!_batchOfProductService.CheckBatchExistence(itemId))
        {
            Console.WriteLine($"Ошибка: Позиция продукта с id {itemId} не найдена.");
            return;
        }
        
        Console.WriteLine("Введите количество товара для покупки:");
        int quantity = int.Parse(Console.ReadLine());



        var itemQuantities = new Dictionary<int, int> { { itemId, quantity } };

        try
        {
            var totalCost = _batchOfProductService.PurchaseItems(itemQuantities);
            Console.WriteLine($"Покупка совершена, денежный итог: {totalCost}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    public void FindCheapestStoreForProduct()
    {

        Console.WriteLine("Введите количество товаров для поиска:");
        int itemCount = int.Parse(Console.ReadLine());

        var products = new List<CheapestStoreModel>();

        for (int i = 0; i < itemCount; i++)
        {
            Console.WriteLine($"Товар {i + 1}:");
            Console.WriteLine("Введите идентификатор продукта:");
            int productId = int.Parse(Console.ReadLine());

            // Проверка существования продукта по введенному идентификатору
            if (!_batchOfProductService.CheckProductExistence(productId))
            {
                Console.WriteLine($"Ошибка: Продукт с указанным идентификатором не найден.");
                return;
            }

            Console.WriteLine("Введите количество товара:");
            int batchCount = int.Parse(Console.ReadLine());

            products.Add(new CheapestStoreModel { ProductId = productId, BatchCount = batchCount });
        }

        try
        {
            var cheapestStore = _batchOfProductService.FindCheapestStoreForBatches(products);
            Console.WriteLine($"Самый дешевый магазин для партии товаров:");

            if (cheapestStore != null)
            {
                Console.WriteLine($"ID магазина: {cheapestStore.Id}");
                Console.WriteLine($"Название магазина: {cheapestStore.Name}");
                Console.WriteLine($"Адрес магазина: {cheapestStore.Address}");
            }
            else
            {
                Console.WriteLine("Магазин не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    
    
}