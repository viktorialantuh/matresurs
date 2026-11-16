namespace MatResurs.Models;

public sealed class InventoryData
{
  // Справочник материалов: названия, цена и остатки.
  // List<Material> — список карточек материалов, который может пополняться.
  public List<Material> Materials { get; set; } = new();

  // История поступления и расхода материалов.
  public List<StockOperation> Operations { get; set; } = new();

  // Счётчики уникальных номеров для новых записей.
  public int NextMaterialId { get; set; } = 1;
  public int NextOperationId { get; set; } = 1;

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

  /// Добавляем материал и сразу отмечаем его начальное количество в истории.
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
    // записывает материал в общий список склада.
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

public sealed class Material
{
  public int Id { get; set; }                 // Уникальный номер
  public string Name { get; set; } = "";      // Название
  public string Unit { get; set; } = "шт.";   // Единица измерения
  public decimal Price { get; set; }          // Цена за одну единицу
  public int MinQuantity { get; set; }        // Минимальное допустимое количество
  public int Quantity { get; set; }           // Текущий остаток на складе
}

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
