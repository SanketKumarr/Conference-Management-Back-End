using ConferenceManagement.Context;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace ConferenceManagement.Business.UserDataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly DbContext _dbContext;
        public UserDataAccess(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            string query = "select * from Users where Email=@Email";
            using (var connection = _dbContext.CreateConnection())
            {
                User user = await connection.QueryFirstOrDefaultAsync<User>(query, new { email });
                return user;
            }
        }

        public async Task<User> LoginUser(LoginUserCommand request)
        {
            string query = "select * from Users where Email=@Email and Password=@Password";
            using (var connection = _dbContext.CreateConnection())
            {
                User user = await connection.QueryFirstOrDefaultAsync<User>(query, new { request.Email, request.Password });
                return user;
            }

        }

        public async Task<User> GetUserById(int user_Id)
        {
            using (var dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM Users WHERE User_Id = @User_Id";
                User CheckUser = await dbConnection.QueryFirstOrDefaultAsync<User>(sQuery, new { User_Id = user_Id });
                dbConnection.Close();
                return CheckUser;
            }
        }

        public async Task<bool> AddUser(User user)
        {
            using (var dbConnection = _dbContext.CreateConnection())
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

        public async Task<bool> UpdateUser(User user)
        {
            using (var dbConnection = _dbContext.CreateConnection())
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
    }
}
