using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.DAL.Models;

namespace ConsoleBestStore.BLL.Services.Shops;

public interface IShopService
{
    void Create(CreateShopModel model);
    void Delete(int storeId);
    void Update(int storeId, UpdateShopModel model);
    List<Shop> GetAllShops();
    Shop GetShopById(int storeId);
}