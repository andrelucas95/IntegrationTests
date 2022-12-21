using Tickets.Domain.Dto;
using Tickets.Domain.Entity;
using Tickets.Domain.Repository;
using Tickets.Infra.Data;
using Tickets.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/tickets", async (ITicketRepository repo) => 
{
    return await repo.List();
});
app.MapPost("/tickets", async (TicketDto dto, ITicketRepository repo) =>
{
    try
    {
        await repo.Insert(new Ticket 
        { 
            Title = dto.Title,
            Description = dto.Description,
            Reference = Guid.NewGuid().ToString()
        });
        
        return Results.Ok();
    }
    catch (Exception)
    {
        return Results.BadRequest();
    }
});

app.Run();
public partial class Program { }
