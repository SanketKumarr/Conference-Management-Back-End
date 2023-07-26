using ConferenceManagement.Context;
using ConferenceManagement.Model;
using Dapper;
using System.Data;

namespace ConferenceManagement.Business.AdminDataAccess
{
    public class AdminDataAccess : IAdminDataAccess
    {
        private readonly DbContext _dbContext;
        public AdminDataAccess(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Get User By Id
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

        #region Delete User
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
    }
}

