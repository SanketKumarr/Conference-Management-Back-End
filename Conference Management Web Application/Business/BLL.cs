using Conference_Management_Web_Application.Business.Interfaces;

namespace Conference_Management_Web_Application.Business
{
    public class BLL : IBLL
    {
        private readonly string _connectionString;
        public BLL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
