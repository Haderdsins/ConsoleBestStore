using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Delete;
using ConsoleBestStore.BLL.Models.Update;

namespace ConsoleBestStore.BLL.Services.Shops;

public interface IShopService
{
    void Create(CreateShopModel model);
    void Delete(int storeId);
    void Update(int storeId, UpdateShopModel model);
}