namespace MatResurs.Models;

public sealed class InventoryData
{
    public List<Material> Materials { get; set; } = new();
    public List<StockOperation> Operations { get; set; } = new();
    public int NextMaterialId { get; set; } = 1;
    public int NextOperationId { get; set; } = 1;

    public static InventoryData CreateDemo()
    {
        var result = new InventoryData();
        result.AddDemo("Бумага А4", "пач.", 420m, 10, 28);
        result.AddDemo("Ручка шариковая", "шт.", 35m, 20, 65);
        result.AddDemo("Папка-регистратор", "шт.", 185m, 5, 12);
        result.AddDemo("Картридж для принтера", "шт.", 2600m, 2, 4);
        return result;
    }

    private void AddDemo(string name, string unit, decimal price, int min, int quantity)
    {
        var item = new Material { Id = NextMaterialId++, Name = name, Unit = unit,
            Price = price, MinQuantity = min, Quantity = quantity };
        Materials.Add(item);
        Operations.Add(new StockOperation { Id = NextOperationId++, At = DateTime.Now.AddDays(-3),
            MaterialName = item.Name, Type = "Начальный остаток", Quantity = quantity,
            BalanceAfter = quantity, Note = "Учебные данные" });
    }
}

public sealed class Material
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Unit { get; set; } = "шт.";
    public decimal Price { get; set; }
    public int MinQuantity { get; set; }
    public int Quantity { get; set; }
}

public sealed class StockOperation
{
    public int Id { get; set; }
    public DateTime At { get; set; }
    public string MaterialName { get; set; } = "";
    public string Type { get; set; } = "";
    public int Quantity { get; set; }
    public int BalanceAfter { get; set; }
    public string Note { get; set; } = "";
}
