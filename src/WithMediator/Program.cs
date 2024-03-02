using Data.Extensions;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WithMediator.Commands;
using WithMediator.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTodoDb();

builder.Services.AddMediator();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapPost("/todo",
    ([FromServices] ISender sender, [FromBody] TodoInputModel model) =>
    {
        sender.Send(new AddTodo.Command(model.Title, model.Description));
    });


app.MapPut("/todo/{id:int}",
    async ([FromServices] ISender sender, [FromBody] TodoInputModel model, [FromRoute] int id) =>
    {
        var result = await sender.Send(new UpdateTodo.Command(id, model.Title, model.Description));

        return result ? Results.Ok() : Results.NotFound();
    });


app.MapDelete("/todo/{id:int}",
    async ([FromServices] ISender sender, [FromRoute] int id) =>
    {
        var result = await sender.Send(new DeleteTodo.Command(id));

        return result ? Results.Ok() : Results.NoContent();
    });

app.Run();