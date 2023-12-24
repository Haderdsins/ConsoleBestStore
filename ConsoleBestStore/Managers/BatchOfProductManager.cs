using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.BLL.Services.BatchOfProducts;


namespace ConsoleBestStore.UI.Visions;

public class BatchOfProductManager
{
    
        private readonly IBatchOfProductService _batchOfProductService;

        public BatchOfProductManager(IBatchOfProductService batchOfProductService)
        {
            _batchOfProductService = batchOfProductService;
        }

        public void RunBatchOfProduct()
        {
            Console.Clear();
            Console.WriteLine("Выберите операцию:");
            Console.WriteLine("1. Создать позицию продукта в магазине");
            Console.WriteLine("2. Удалить позицию продукта в магазине");
            Console.WriteLine("3. Завести партию товара в магазин");
            Console.WriteLine("4. Найти магазин с минимальным ценником на выбранный продукт");
            Console.WriteLine("5. Понять, какие товары можно купить в магазине на некоторую сумму");
            Console.WriteLine("6. Купить партию товаров в магазине");
            Console.WriteLine("7. Найти, в каком магазине партия товаров");
            int choice = int.Parse(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    CreateBatchOfProduct();
                    break;
                case 2:
                    DeleteBatchOfProduct();
                    break;
                case 3:
                    UpdateBatchOfProduct();
                    break;
                case 4:
                    FoundStoreWhereMinPriceProduct();
                    break;
                case 5:
                    GetItemsInPrice();
                    break;
                case 6:
                    PurchaseBatchOfProduct();
                    break;
                case 7:
                    FindCheapestStoreForProduct();
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
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
                //Console.WriteLine($"{item.Item.ProductId}. {item.Item.Product.Name} - {item.Quantity} шт.");
            }
        }

        public void CreateBatchOfProduct()
        {
            Console.WriteLine("Введите идентификатор магазина:");
            int storeId = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите идентификатор продукта:");
            int productId = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите количество товара:");
            int count = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите цену товара:");
            int price = int.Parse(Console.ReadLine());

            var createModel = new CreateBatchOfProductModel { StoreId = storeId, ProductId = productId, Count = count, Price = price };

            try
            {
                _batchOfProductService.CreateBatchOfProduct(createModel);
                Console.WriteLine("Позиция продукта в магазине создана успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void DeleteBatchOfProduct()
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

        public void UpdateBatchOfProduct()
        {
            Console.WriteLine("Введите идентификатор позиции продукта в магазине для обновления:");
            int itemId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество привоза товара:");
            int newCount = int.Parse(Console.ReadLine());

            try
            {
                var updateModel = new UpdateBatchOfProductModel { Count = newCount };
                _batchOfProductService.Update(itemId, updateModel);
                Console.WriteLine("Позиция продукта в магазине обновлена успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public void FindCheapestStoreForProduct()
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

        public void PurchaseBatchOfProduct()
        {
            Console.WriteLine("Введите идентификатор позиции продукта в магазине для покупки:");
            int itemId = int.Parse(Console.ReadLine());

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
}