using ConferenceManagement.Business.Token;
using ConferenceManagement.Context;
using ConferenceManagement.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        #region Delete Request By Book Id
        /// <summary>
        /// Mohit :- Delete Request By Book Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRequestByBookId(int bookId)
        {
            string query = "delete from BookRoom where BookingId=@BookingId";
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                int deleteRequestStatus = await connection.ExecuteAsync(query, new { BookingId = bookId });
                if (deleteRequestStatus > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


        #region Get All User
        /// <summary>
        /// Mohit :- Get All User
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Ashish :- Delete User
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Ashish :- Get User By Id
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
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


        #region Add Room
        /// <summary>
        /// Ashish :- Add Room
        /// </summary>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>

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


        #region Get Room By Id
        /// <summary>
        /// Ashish :- Get Room By Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
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


        #region Update Room
        /// <summary>
        /// Ashish :- UpdateRoom
        /// </summary>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>
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


        #region Delete Room
        /// <summary>
        /// Ashish :- Delete Room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Mohit :- Get All Pending Request
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<BookRoom>> GetAllPendingRequest(string status)
        {
            string query = "select * from BookRoom where Status=@Status";
            using (IDbConnection connetion = _dbContext.CreateConnection())
            {
                IEnumerable<BookRoom> allPendingRequest = await connetion.QueryAsync<BookRoom>(query, new { status });
                return allPendingRequest.ToList();
            }
        }

        public async Task<ConferenceRoom> GetRoomByRoomId(int roomId)
        {
            string query = "select * from ConferenceRoom where RoomId=@RoomId";
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                ConferenceRoom roomDetails = await connection.QueryFirstOrDefaultAsync<ConferenceRoom>(query, new { roomId });
                return roomDetails;
            }
        }

        public async Task<BookRoom> GetBookingDetailsById(int bookingId)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "Select * From BookRoom WHERE BookingId = @BookingId";
                BookRoom roomDetails = await dbConnection.QueryFirstOrDefaultAsync<BookRoom>(sQuery, new { BookingId = bookingId });
                dbConnection.Close();
                return roomDetails;
            }
        }

        public async Task<List<BookRoom>> GetAllBookings()
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "select * from BookRoom";
                IEnumerable<BookRoom> bookRooms = await dbConnection.QueryAsync<BookRoom>(sQuery);
                dbConnection.Close();
                return bookRooms.ToList();
            }
        }

        public async Task<BookRoom> GetRequestByBookId(int bookId)
        {
            string query = "select * from BookRoom where BookingId=@BookingId";
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                BookRoom getRequest = await connection.QueryFirstOrDefaultAsync<BookRoom>(query, new { bookingId = bookId });
                return getRequest;
            }
        }
        #endregion


        #region Get All Contact
        /// <summary>
        /// Sachin :- Get All Contact
        /// </summary>
        /// <returns></returns>
        public async Task<List<Contact>> GetAllContact()
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "Select * From Contact";
                List<Contact> AllContact = (await dbConnection.QueryAsync<Contact>(sQuery)).ToList();
                dbConnection.Close();
                return AllContact;
            }
        }
        #endregion


        #region Get All Notification
        /// <summary>
        /// Mansi :- Get All Notification
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Notification>> GetAllNotification()
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "Select * From Notification";
                List<Notification> AllNotification = (await dbConnection.QueryAsync<Notification>(sQuery)).ToList();
                dbConnection.Close();
                return AllNotification;
            }
        } 
        #endregion
    }
}
