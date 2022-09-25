using System;
using Microsoft.AspNetCore.Builder;
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

app.MapFallbackToFile("index.html");

app.Run();