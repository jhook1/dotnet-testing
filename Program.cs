using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");
app.MapFallbackToFile("index.html");

app.Run();