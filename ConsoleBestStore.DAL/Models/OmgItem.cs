namespace ConsoleBestStore.DAL.Models;

public class OmgItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }//идентификатор продукта

    public int StoreId { get; set; } //идентификатор магазина, где продается

    public int Count { get; set; }//Количество продукта

    public int Price { get; set; }//Цена продукта
}