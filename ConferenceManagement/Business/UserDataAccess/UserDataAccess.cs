using ConferenceManagement.Context;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using Dapper;
using MediatR;
using System.Data;
using System.Security;
using System.Security.Cryptography;
using System.Text;

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
            string salt = await GetSaltByEmail(request.Email);
            string hashedPasssword = HashPassword(request.Password, salt);
            string query = "select * from Users where Email=@Email and Password=@HashedPasssword";
            using (var connection = _dbContext.CreateConnection())
            {
                User user = await connection.QueryFirstOrDefaultAsync<User>(query, new { request.Email, hashedPasssword });
                return user;
            }
        }
        #endregion

        public async Task<string> GetSaltByEmail(string email)
        {
            string query = "select Salt from Users where Email = @Email";
            using (var connection = _dbContext.CreateConnection())
            {
                string salt = await connection.QueryFirstOrDefaultAsync<string>(query, new { Email = email });
                return salt;
            }
        }


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
                string salt = GenerateSalt();
                user.Password = HashPassword(user.Password, salt);
                string sQuery = "INSERT INTO Users (Name, Email, Password, Designation, Salt) VALUES (@Name, @Email, @Password, @Designation, @Salt)";
                int count = await dbConnection.ExecuteAsync(sQuery, new
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Designation = user.Designation,
                    Salt = salt
                });
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
                int count = await dbConnection.ExecuteAsync(sQuery, new
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Designation = user.Designation
                });
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

        #region Room Notification
        /// <summary>
        /// Sachin :- Room Notification
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<BookRoom>> RoomNotification(int userId)
        {
            using (IDbConnection dbConnection = _dbContext.CreateConnection())
            {
                dbConnection.Open();
                string sQuery = "select * from BookRoom Where UserId = @UserId order by BookingId desc";
                List<BookRoom> AllBookRooms = (await dbConnection.QueryAsync<BookRoom>(sQuery, new { UserId = userId })).ToList();
                dbConnection.Close();
                return AllBookRooms;
            }
        }
        #endregion

        #region PasswordHashing
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);

            using (var sha256 = SHA256.Create())
            {
                byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
        #endregion
    }
}
