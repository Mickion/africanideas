using afi.university.ui;
using afi.university.ui.Services.Implementations;
using afi.university.ui.Services.Implementations.Authentication;
using afi.university.ui.Services.Implementations.HttpService;
using afi.university.ui.Services.Interfaces;
using afi.university.ui.Services.Interfaces.Authentication;
using afi.university.ui.Services.Interfaces.HttpService;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredToast();

builder.Services
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<ILocalStorageService, LocalStorageService>()
    .AddScoped<IStudentService, StudentService>()
    .AddScoped<ICourseService, CourseService>()
    .AddScoped<IHttpService, HttpService>();

// configure http client
builder.Services.AddScoped(x => {
    var apiUrl = new Uri(builder.Configuration["apiUrl"]);
    return new HttpClient() { BaseAddress = apiUrl };
});


await builder.Build().RunAsync();
