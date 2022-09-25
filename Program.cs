using System;
using System.IO;
using System.Text;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using dotnet_testing;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseStaticFiles();

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

app.MapFallbackToFile("index.html");

app.Run();