using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Quiz.Application;
using Quiz.Application.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://quizzaocznegrupa6.azurewebsites.net/") });
await builder.Build().RunAsync();
