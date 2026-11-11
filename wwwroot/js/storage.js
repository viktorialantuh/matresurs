// Небольшой JavaScript-мост для браузерного хранилища.
// Все операции с материалами и расчеты написаны на C#.
window.matresursStorage = {
  load: () => localStorage.getItem('matresurs-demo-v1'),
  save: (json) => localStorage.setItem('matresurs-demo-v1', json),
  clear: () => localStorage.removeItem('matresurs-demo-v1'),
  askReset: () => window.confirm('Вернуть демонстрационные данные? Ваши изменения будут удалены.'),
  download: (name, content) => {
    const blob = new Blob([content], {type: 'text/csv;charset=utf-8'});
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url; a.download = name; document.body.appendChild(a);
    a.click(); a.remove(); URL.revokeObjectURL(url);
  }
};
