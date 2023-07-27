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

        /// <summary>
        /// Mohit :- Delete Request By Book Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Mohit :- Get All User
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUser()
        {
            string query = "SELECT * FROM Users WHERE IsAdmin = 'false'";
            using(var connection = _dbContext.CreateConnection())
            {
                IEnumerable<User> allUser =await connection.QueryAsync<User>(query);
                return allUser.ToList();
            }
        }

        /// <summary>
        /// Ashish :- Delete User
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Ashish :- Get User By Id
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Ashish :- Add Room
        /// </summary>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>
        #region Add Room
        public async Task<bool> AddRoom(ConferenceRoom conferenceRoom)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "INSERT INTO ConferenceRoom (RoomName, Capacity, IsAVRoom, Image) VALUES (@RoomName, @Capacity, @IsAVRoom, @Image)";
                int count = await dbConnection.ExecuteAsync(sQuery, new { conferenceRoom.RoomName, conferenceRoom.Capacity, conferenceRoom.IsAVRoom, conferenceRoom.Image });
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


        /// <summary>
        /// Ashish :- Get Room By Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        #region Get Room By Id
        public async Task<ConferenceRoom> GetRoomById(int roomId)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM ConferenceRoom WHERE RoomId = @RoomId";
                ConferenceRoom CheckConferenceRoom = await dbConnection.QueryFirstOrDefaultAsync<ConferenceRoom>(sQuery, new { RoomId = roomId });
                dbConnection.Close();
                return CheckConferenceRoom;
            }
        }
        #endregion



        /// <summary>
        /// Ashish :- UpdateRoom
        /// </summary>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>
        #region Update Room
        public async Task<bool> UpdateRoom(ConferenceRoom conferenceRoom)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "UPDATE ConferenceRoom SET RoomName = @RoomName, Capacity = @Capacity, IsAVRoom = @IsAVRoom, Image = @Image  WHERE RoomId = @RoomId";
                int count = await dbConnection.ExecuteAsync(sQuery, conferenceRoom);
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Ashish :- DeleteRoom
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        #region Delete Room
        public async Task<bool> DeleteRoom(int roomId)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "DELETE FROM ConferenceRoom WHERE RoomId = @RoomId";
                int count = await dbConnection.ExecuteAsync(sQuery, new { RoomId = roomId });
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
