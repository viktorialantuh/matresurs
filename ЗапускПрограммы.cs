// Точка входа: программа на C# запускается через Blazor WebAssembly в браузере.
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MatResurs;

// Создаём среду выполнения приложения.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// App.razor отображается внутри блока <div id="app"> в index.html.
builder.RootComponents.Add<App>("#app");

// Позволяет менять заголовок и другие элементы <head>.
builder.RootComponents.Add<HeadOutlet>("head::after");

// Собираем и запускаем приложение.
await builder.Build().RunAsync();
