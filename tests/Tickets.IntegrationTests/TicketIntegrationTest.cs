using System.Data;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Tickets.Domain.Dto;
using Tickets.Domain.Entity;
using Tickets.Domain.Repository;
using Tickets.Infra.Data;
using Tickets.IntegrationTests.Common;

namespace Tickets.IntegrationTests;

public class TicketIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _webApp;
    private readonly HttpClient _httpClient;
    private readonly ITicketRepository _ticketRepository;
    private readonly IDbConnection _connection;

    public TicketIntegrationTest()
    {
        _webApp = new CustomWebApplicationFactory<Program>();
        _httpClient = _webApp.CreateClient();
        _ticketRepository = _webApp.Services.GetService<ITicketRepository>() ?? throw new NullReferenceException();
        _connection = _webApp.Services.GetService<IConnectionFactory>()?.CreateConnection() ?? throw new NullReferenceException();

        new TicketDatabaseSeed().EnsureDatabase(_connection);
    }

    [Fact]
    public async Task GetTickets_Should_Not_BeEmpty()
    {
        var response = await _httpClient.GetAsync("/tickets");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var tickets = await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>();
        Assert.NotEmpty(tickets);
    }

    [Fact]
    public async Task InsertTicket_Should_Be_Success()
    {
        var response = await _httpClient
            .PostAsJsonAsync("/tickets", new TicketDto() { Title = "Example title", Description = "Example description" });
        var tickets = await _ticketRepository
            .List();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(tickets);
    }
}