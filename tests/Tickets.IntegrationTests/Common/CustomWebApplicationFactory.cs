using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Tickets.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using Tickets.Domain.Repository;
using Tickets.Infra.Repository;

namespace Tickets.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => 
            {
                var sqlConnectionFactoryDescriptor = services
                    .Single(s => s.ServiceType == typeof(IConnectionFactory));

                var ticketRepositoryDescriptor = services
                    .Single(s => s.ServiceType == typeof(ITicketRepository));
                
                services.Remove(sqlConnectionFactoryDescriptor);
                services.Remove(ticketRepositoryDescriptor);
                services.AddSingleton<IConnectionFactory>(s => new InMemoryConnectionFactory());
                services.AddSingleton<ITicketRepository, TicketRepository>();
            });
        }
    }
}