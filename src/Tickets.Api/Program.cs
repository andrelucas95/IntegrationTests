using Tickets.Domain.Dto;
using Tickets.Domain.Entity;
using Tickets.Domain.Repository;
using Tickets.Infra.Data;
using Tickets.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IConnectionFactory, PostgresConnectionFactory>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

var app = builder.Build();

app.MapGet("/", () => "Tickets Api Running!");
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
            Reference = Guid.NewGuid().ToString("N")
        });
        
        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();
public partial class Program { }
