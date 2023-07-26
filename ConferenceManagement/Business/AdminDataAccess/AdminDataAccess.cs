using ConferenceManagement.Business.Token;
using ConferenceManagement.Context;
using ConferenceManagement.Model;
using Dapper;
using System.Data;

namespace ConferenceManagement.Business.AdminDataAccess
{
    public class AdminDataAccess : IAdminDataAccess
    {
        private readonly string _connectionString;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly DbContext _dbContext;

        public AdminDataAccess(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region GetAllUser
        public async Task<List<User>> GetAllUser()
        {
            string query = "SELECT * FROM Users WHERE IsAdmin = 'false'";
            using (var connection = _dbContext.CreateConnection())
            {
                IEnumerable<User> allUser = await connection.QueryAsync<User>(query);
                return allUser.ToList();
            }
        }
        #endregion

        #region DeleteUser
        public async Task<bool> DeleteUser(int user_Id)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "DELETE FROM Users WHERE User_Id = @User_Id AND IsAdmin = 'false'";
                int count = await dbConnection.ExecuteAsync(sQuery, new { User_Id = user_Id });
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


        #region GetUserById
        public async Task<User> GetUserById(int user_Id)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM Users WHERE User_Id = @User_Id";
                User checkUser = await dbConnection.QueryFirstOrDefaultAsync<User>(sQuery, new { User_Id = user_Id });
                dbConnection.Close();
                return checkUser;
            }
        } 
        #endregion
    }
}
