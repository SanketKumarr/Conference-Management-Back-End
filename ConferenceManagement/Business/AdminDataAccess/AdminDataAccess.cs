namespace ConferenceManagement.Business.AdminDataAccess
{
    public class AdminDataAccess : IAdminDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AdminDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LocalDbConnection");
        }
    }
}
