using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.DAL.Models;

namespace ConsoleBestStore.BLL.Services.Products;

public interface IProductService
{
    void Create(CreateProductModel model);
    void Delete(int productId);
    void Update(int productId, UpdateProductModel model);
    
    List<Product> GetAllProducts();
}