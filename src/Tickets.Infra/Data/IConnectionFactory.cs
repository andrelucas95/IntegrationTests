using System.Data;

namespace Tickets.Infra.Data
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}