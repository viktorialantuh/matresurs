// Исходный скрипт из папки «Скрипты( сохранение данных и скачивание отчётов)».
// При публикации GitHub копирует его в wwwroot.
// Вспомогательные функции браузера. Складская логика написана на C#.
// Важно: данные сохраняются отдельно в браузере каждого компьютера.
window.matresursStorage = {
  // Загрузка материалов и истории из localStorage.
  load: () => localStorage.getItem("matresurs-demo-v1"),

  // Сохранение результатов работы программы.
  save: (json) => localStorage.setItem("matresurs-demo-v1", json),

  // Удаление сохранённых данных (используется при необходимости сброса).
  clear: () => localStorage.removeItem("matresurs-demo-v1"),

  // Спрашиваем подтверждение перед сбросом материалов.
  askReset: () => window.confirm(
    "Вернуть демонстрационные данные? Ваши изменения будут удалены."
  ),

  // Создаём и скачиваем отчёт, который подготовил C#.
  download: (name, content) => {
    // Blob — временный файл прямо в памяти браузера.
    const blob = new Blob([content], { type: "text/csv;charset=utf-8" });
    const url = URL.createObjectURL(blob);
    const link = document.createElement("a");

    // Создаём ссылку и автоматически нажимаем её для скачивания.
    link.href = url;
    link.download = name;
    document.body.appendChild(link);
    link.click();

    // Освобождаем временные ресурсы.
    link.remove();
    URL.revokeObjectURL(url);
  }
};
