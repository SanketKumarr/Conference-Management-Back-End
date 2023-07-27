using ConferenceManagement.Business.Token;
using ConferenceManagement.Context;
using ConferenceManagement.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ConferenceManagement.Business.AdminDataAccess
{
    public class AdminDataAccess : IAdminDataAccess
    {
        //private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly DbContext _dbContext;
        public AdminDataAccess(DbContext dbContext)//IConfiguration configuration
        {
            _dbContext = dbContext;
            // _configuration = configuration;
            // _connectionString = _configuration.GetConnectionString("LocalDbConnection");

        }

        public async Task<bool> DeleteRequestByBookId(int bookId)
        {
            string query = "delete from BookRoom where BookingId=@bookId";
            using(IDbConnection connection=_dbContext.CreateConnection())
            {
                int deleteRequestStatus= await connection.ExecuteAsync(query, new {bookId});
                if (deleteRequestStatus > 0) 
                { 
                    return true;
                }
                return false;
            }
        }

        public async Task<List<User>> GetAllUser()
        {
            string query = "SELECT * FROM Users WHERE IsAdmin = 'false'";
            using(var connection = _dbContext.CreateConnection())
            {
                IEnumerable<User> allUser =await connection.QueryAsync<User>(query);
                return allUser.ToList();
            }
        }
    }
}
