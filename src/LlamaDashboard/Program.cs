using LlamaDashboard.Services;
using LlamaDashboard.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register HttpClient with base address for llama.cpp API
builder.Services.AddHttpClient<ILlamaCppService, LlamaCppService>(client =>
{
    var config = builder.Configuration;
    var baseUrl = config.GetValue<string>("LlamaCpp:Url") ?? "http://localhost:8080/v1";
    client.BaseAddress = new Uri(baseUrl);
});

// Register ConfigService as singleton
builder.Services.AddSingleton<ConfigService>();

// Register other services
builder.Services.AddScoped<ILlamaCppService, LlamaCppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();
