// Папка «Модели»: классы, описывающие материалы и складские операции.
// Namespace оставлен техническим MatResurs.Models, чтобы не менять имена типов.
namespace MatResurs.Models;

/// <summary>Все материалы и журнал операций.</summary>
public sealed class InventoryData
{
    // Справочник материалов: названия, цена и остатки.
    public List<Material> Materials { get; set; } = new();

    // История поступления и расхода материалов.
    public List<StockOperation> Operations { get; set; } = new();

    // Счётчики уникальных номеров для новых записей.
    public int NextMaterialId { get; set; } = 1;
    public int NextOperationId { get; set; } = 1;

    /// <summary>Начальные учебные материалы, доступные после первого запуска.</summary>
    public static InventoryData CreateDemo()
    {
        var result = new InventoryData();
        // Название, единица, цена, минимальный остаток, количество.
        result.AddDemo("Бумага А4", "пач.", 420m, 10, 28);
        result.AddDemo("Ручка шариковая", "шт.", 35m, 20, 65);
        result.AddDemo("Папка-регистратор", "шт.", 185m, 5, 12);
        result.AddDemo("Картридж для принтера", "шт.", 2600m, 2, 4);
        return result;
    }

    /// <summary>Добавляем материал и сразу отмечаем его начальное количество в истории.</summary>
    private void AddDemo(string name, string unit, decimal price, int min, int quantity)
    {
        var item = new Material
        {
            Id = NextMaterialId++,
            Name = name,
            Unit = unit,
            Price = price,
            MinQuantity = min,
            Quantity = quantity
        };
        Materials.Add(item);

        Operations.Add(new StockOperation
        {
            Id = NextOperationId++,
            At = DateTime.Now.AddDays(-3),
            MaterialName = item.Name,
            Type = "Начальный остаток",
            Quantity = quantity,
            BalanceAfter = quantity,
            Note = "Учебные данные"
        });
    }
}

/// <summary>Одна позиция из справочника материальных ресурсов.</summary>
public sealed class Material
{
    public int Id { get; set; }                 // Уникальный номер
    public string Name { get; set; } = "";      // Название
    public string Unit { get; set; } = "шт.";   // Единица измерения
    public decimal Price { get; set; }          // Цена за одну единицу
    public int MinQuantity { get; set; }        // Минимальное допустимое количество
    public int Quantity { get; set; }           // Текущий остаток на складе
}

/// <summary>Одна запись о поступлении или выдаче материала.</summary>
public sealed class StockOperation
{
    public int Id { get; set; }                 // Номер операции
    public DateTime At { get; set; }            // Дата и время
    public string MaterialName { get; set; } = ""; // Материал
    public string Type { get; set; } = "";      // Приход, расход или начальный остаток
    public int Quantity { get; set; }           // Изменённое количество
    public int BalanceAfter { get; set; }       // Остаток после операции
    public string Note { get; set; } = "";      // Дополнительный комментарий
}
