using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.DAL.DataBase;
using ConsoleBestStore.DAL.Models;

namespace ConsoleBestStore.BLL.Services.Products;

public class ProductService : IProductService
{
    
    private readonly ShopDbContext _dbContext;

    public ProductService(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Create(CreateProductModel model)
    {
        var product = new Product();
        product.Name = model.Name;


        // logic to add database
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }
    public void Delete(int ProductId)
    {
        var shopToDelete = _dbContext.Shops.Find(ProductId);

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
    public void Update(int productId, UpdateProductModel model)
    {
        var productToUpdate = _dbContext.Products.Find(productId);

        if (productToUpdate != null)
        {
            // Обновление атрибутов продукта
            productToUpdate.Name = model.Name; // Пример, добавьте другие атрибуты, которые необходимо обновить

            _dbContext.SaveChanges();
        }
        else
        {
            // Обработка случая, когда продукт с указанным ID не найден
            throw new ArgumentException("Продукт не найден.");
        }
    }
    public List<Product> GetAllProducts()
    {
        return _dbContext.Products.ToList();
    }
}