using System.Data;
using System.Data.SqlClient;

namespace ConferenceManagement.Context
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _DbConnection;
        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _DbConnection = _configuration.GetConnectionString("LocalDbConnection");
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_DbConnection);
        }
    }
}
