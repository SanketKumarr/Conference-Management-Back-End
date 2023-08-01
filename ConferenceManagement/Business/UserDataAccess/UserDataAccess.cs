using ConferenceManagement.Context;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using Dapper;
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

        #region Book Room
        /// <summary>
        /// Mohit :- Book Room
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BookRoom(BookRoomCommand request)
        {
            string query = "insert into BookRoom (RequestId,UserId,RoomId,Date,TimeSlot,Status) values(@RequestId,@UserId,@RoomId,@Date,@TimeSlot,@Status)";
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                int bookStatus = await connection.ExecuteAsync(query, new { request.RequestId, request.UserId, request.RoomId, request.Date, request.TimeSlot, request.Status });
                if (bookStatus > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


        #region Get User By Email
        /// <summary>
        /// Mohit :- Get User By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            string query = "select * from Users where Email=@Email";
            using (var connection = _dbContext.CreateConnection())
            {
                User user = await connection.QueryFirstOrDefaultAsync<User>(query, new { email });
                return user;
            }
        }
        #endregion


        #region Login User
        /// <summary>
        /// Mohit :- Login User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<User> LoginUser(LoginUserCommand request)
        {
            string query = "select * from Users where Email=@Email and Password=@Password";
            using (var connection = _dbContext.CreateConnection())
            {
                User user = await connection.QueryFirstOrDefaultAsync<User>(query, new { request.Email, request.Password });
                return user;
            }
        }
        #endregion


        #region Get User By Id
        /// <summary>
        /// Ashish :- Get User By Id
        /// </summary>
        /// <param name="user_Id"></param>
        /// <returns></returns>
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
        #endregion


        #region Add User
        /// <summary>
        /// Ashish :- Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
        #endregion


        #region Update User
        /// <summary>
        /// Ashish :- Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
        #endregion


        #region Display All Room
        /// <summary>
        /// Ashish :- Display All Room
        /// </summary>
        /// <returns></returns>
        public async Task<List<ConferenceRoom>> DisplayAllRoom()
        {
            using (var dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "SELECT * FROM ConferenceRoom";
                List<ConferenceRoom> AllConferenceRooms = (await dbConnection.QueryAsync<ConferenceRoom>(sQuery)).ToList();
                dbConnection.Close();
                return AllConferenceRooms;
            }
        }
        #endregion


        #region Get Room By Room Id
        /// <summary>
        /// :- Get Room By Room Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public async Task<ConferenceRoom> GetRoomByRoomId(int roomId)
        {
            string query = "select * from ConferenceRoom where RoomId=@RoomId";
            using (IDbConnection connection = _dbContext.CreateConnection())
            {
                ConferenceRoom conferenceRoom = await connection.QueryFirstOrDefaultAsync<ConferenceRoom>(query, new { RoomId = roomId });
                return conferenceRoom;
            }
        }
        #endregion


        #region Get Room By Booking Id
        /// <summary>
        /// :- Get Room By Booking Id
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public async Task<BookRoom> GetRoomByBookingId(int bookingId)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string query = "select * from BookRoom where BookingId = @BookingId";
                BookRoom bookRoom = await dbConnection.QueryFirstOrDefaultAsync<BookRoom>(query, new { BookingId = bookingId });
                dbConnection.Close();
                return bookRoom;
            }
        }
        #endregion


        #region Cancle Room
        /// <summary>
        /// Mansi :- Cancle Room
        /// </summary>
        /// <param name="checkbookRoom"></param>
        /// <returns></returns>
        public async Task<bool> CancleRoom(BookRoom checkbookRoom)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string query = "UPDATE BookRoom SET Status = @Status WHERE BookingId = @BookingId";
                int count = await dbConnection.ExecuteAsync(query, checkbookRoom);
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


        #region Check Contact By Email
        /// <summary>
        /// Mansi :- Check Contact By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Contact> CheckContactByEmail(string email)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "Select * From Contact Where Email = @Email";
                Contact contact = await dbConnection.QueryFirstOrDefaultAsync<Contact>(sQuery, new { Email = email });
                dbConnection.Close();
                return contact;
            }
        }
        #endregion

        #region Add Contact
        /// <summary>
        /// Mansi :- Add Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> AddContact(Contact contact)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "INSERT INTO Contact (Name, Email, Phone, Message) VALUES (@Name, @Email, @Phone, @Message)";
                int count = await dbConnection.ExecuteAsync(sQuery, contact);
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
