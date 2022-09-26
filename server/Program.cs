using System;
using System.IO;
using System.Text;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using dotnet_testing;

var builder = WebApplication.CreateBuilder();
builder.Services.AddCors((options) =>
{
    options.AddPolicy("AllowLocalHostPolicy", (policy) =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.WithHeaders("content-type");
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseCors("AllowLocalHostPolicy");

app.MapGet("/stuff", () => { Console.WriteLine("Get!"); return "Hello World!"; });
app.MapGet("/stuff/{id}", (int id) => $"Stuff #: {id}");
app.MapPost("/stuff", (SimpleModel obj) => { 
    Console.WriteLine("Post!");
    return $"Obj ({obj.Id}) Name: {obj.Name} Description: {obj.Description}"; 
});
app.MapGet("/stuff/form", (HttpContext context) =>
{
    string htmlText = File.ReadAllText("wwwroot/post.html");
    context.Response.ContentType = MediaTypeNames.Text.Html;
    context.Response.ContentLength = Encoding.UTF8.GetByteCount(htmlText);
    context.Response.WriteAsync(htmlText);
});
app.MapGet("/stuff/form/redirect", (HttpContext context) =>
{
    Console.WriteLine("Redirect!");
    context.Response.Redirect("http://localhost:3000");
});

app.MapFallbackToFile("index.html");

app.Run();