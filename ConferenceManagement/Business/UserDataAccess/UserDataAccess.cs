using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using Dapper;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ConferenceManagement.Business.UserDataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UserDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LocalDbConnection");
        }

        #region Add User
        public async Task<bool> AddUser(User user)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "INSERT INTO Users (Name, Email, Password, Designation) VALUES (@Name, @Email, @Password, @Designation)";
                int count = await dbConnection.ExecuteAsync(sQuery, user);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion


        #region Get User By Id
        public async Task<User> GetUserById(int user_Id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM Users WHERE User_Id = @User_Id";
                User CheckUser = await dbConnection.QueryFirstOrDefaultAsync<User>(sQuery, new { User_Id = user_Id });
                dbConnection.Close();
                return CheckUser;
            }
        }
        #endregion


        #region Update User
        public async Task<bool> UpdateUser(User user)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                string sQuery = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password, Designation = @Designation WHERE User_Id = @User_Id";
                int count = await dbConnection.ExecuteAsync(sQuery, user);
                dbConnection.Close();
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}
