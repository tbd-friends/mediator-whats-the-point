using Data.Extensions;
using Microsoft.AspNetCore.Mvc;
using NonMediator.Models;
using NonMediator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTodoDb();
builder.Services.AddScoped<TodoService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/todo",
    ([FromServices] TodoService service, [FromBody] TodoInputModel model) => { service.AddTodo(model); });
app.MapPut("/todo/{id:int}",
    ([FromServices] TodoService service, [FromBody] TodoInputModel model, [FromRoute] int id) =>
        service.UpdateTodo(id, model) ? Results.Ok() : Results.NotFound());
app.MapDelete("/todo/{id:int}",
    ([FromServices] TodoService service, [FromRoute] int id) =>
        service.DeleteTodo(id) ? Results.Ok() : Results.NotFound());

app.Run();