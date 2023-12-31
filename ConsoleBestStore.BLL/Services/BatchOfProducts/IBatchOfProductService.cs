﻿using ConsoleBestStore.BLL.Models.Create;
using ConsoleBestStore.BLL.Models.Get;
using ConsoleBestStore.BLL.Models.Search;
using ConsoleBestStore.BLL.Models.Update;
using ConsoleBestStore.DAL.Models;

namespace ConsoleBestStore.BLL.Services.BatchOfProducts;

public interface IBatchOfProductService
{
    void CreateBatchOfProduct(CreateBatchOfProductModel model);
    
    void Delete(int itemId);
    Shop FoundStoreWhereMinPriceProduct(int productId);
    
    GetItemsForAmountModel GetItemsForAmount(decimal amount);
    
    void Update(int itemId, UpdateBatchOfProductModel model);

    decimal PurchaseItems(Dictionary<int, int> itemQuantities);
    
    GetAllStoresModel FindCheapestStoreForBatches(List<CheapestStoreModel> model);
    bool CheckStoreExistence(int storeId);
    bool CheckProductExistence(int productId);
    bool CheckItemExistence(int itemId);
    Product GetProductById(int productId);
    Shop GetShopById(int storeId);
    bool CheckBatchExistence(int itemId);
    
    
    
}