// При публикации GitHub копирует его в wwwroot.
window.matresursStorage = {
  // Загрузка материалов и истории из localStorage.
  // getItem читает запись по ключу.
  load: () => localStorage.getItem("matresurs-demo-v1"),

  // Сохранение результатов работы программы.
  // setItem перезаписывает сохранённую копию материалов и журнала.
  save: (json) => localStorage.setItem("matresurs-demo-v1", json),

  // Удаление сохранённых данных (используется при необходимости сброса).
  clear: () => localStorage.removeItem("matresurs-demo-v1"),

  // Спрашиваем подтверждение перед сбросом материалов.
  askReset: () => window.confirm(
    "Вернуть демонстрационные данные? Ваши изменения будут удалены."
  ),

  // Формируем Excel с числовыми ячейками, ценами и итогом.
  downloadExcel: (name, rows) => {
    if (!window.XLSX) throw new Error("Библиотека Excel не загрузилась");
    const sheet = XLSX.utils.aoa_to_sheet(rows);
    sheet["!cols"] = [{ wch: 30 }, { wch: 15 }, { wch: 15 }, { wch: 16 }, { wch: 18 }];
    for (let i = 1; i < rows.length; i++)
      for (const col of ["D", "E"]) {
        const cell = sheet[col + (i + 1)];
        if (cell && cell.t === "n") cell.z = "#,##0.00";
      }
    const book = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(book, sheet, "Остатки");
    XLSX.writeFile(book, name);
  }
};
