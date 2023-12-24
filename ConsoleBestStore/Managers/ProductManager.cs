using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Delete;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.BLL.Services.Products;

namespace ConsoleBestStore.UI.Visions;

public class ProductManager
{
    
    private IProductService _productService;

    public ProductManager(IProductService productService)
    {
        _productService = productService;
    }

    public void RunProduct()
    {
        Console.Clear();
        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Создать продукт");
        Console.WriteLine("2. Удалить продукт");
        Console.WriteLine("3. Обновить продукт");
        Console.WriteLine("4. Вывести все продукты");
        Console.WriteLine("5. Назад");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                CreateProduct();
                RunProduct();
                break;
            case 2:
                DeleteProduct();
                RunProduct();
                break;
            case 3:
                UpdateProduct();
                RunProduct();
                break;
            case 4:
                DisplayAllProducts();
                RunProduct();
                break;
            case 5:
                Program.Main();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }

    public void CreateProduct()
    {
        Console.WriteLine("Введите название продукта:");
        string name = Console.ReadLine();

        var createStoreModel = new CreateProductModel() { Name = name };
        _productService.Create(createStoreModel);

        Console.WriteLine("Продукт создан успешно!");
    }
    public void DeleteProduct()
    {
        Console.WriteLine("Введите идентификатор продукта для удаления:");
        int productId = int.Parse(Console.ReadLine());
        
        var deleteProductModel = new DeleteProductModel() { ProductId = productId };
        _productService.Delete(deleteProductModel.ProductId);

        Console.WriteLine("Продукт удален успешно!");
    }

    public void UpdateProduct()
    {
        Console.WriteLine("Введите идентификатор продукта для обновления:");
        int productId = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите новое название продукта:");
        string newName = Console.ReadLine();

        var updateProductModel = new UpdateProductModel() { ProductId = productId, Name = newName};
        _productService.Update(updateProductModel.ProductId, updateProductModel);
        Console.WriteLine("Продукт обновлен успешно!");
    }
    private void DisplayAllProducts()
    {
        var products = _productService.GetAllProducts();
    
        if (products.Any())
        {
            Console.WriteLine("Список всех продуктов:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Продукт: {products[i].Name}");
            }
        }
        else
        {
            Console.WriteLine("Нет доступных продуктов.");
        }
    }

}