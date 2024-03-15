using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data;

namespace MIMPScheme.Repository
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection(IOptions<RepositoryOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        // Your repository methods go here...

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
