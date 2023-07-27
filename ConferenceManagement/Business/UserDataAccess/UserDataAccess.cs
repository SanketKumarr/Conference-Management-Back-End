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

        /// <summary>
        /// Mohit :- Book Room
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BookRoom(BookRoomCommand request)
        {
            string query = "insert into BookRoom (RequestId,UserId,RoomId,Date,TimeSlot,Status) values(@RequestId,@UserId,@RoomId,@Date,@TimeSlot,@Status)";
            using(IDbConnection connection=_dbContext.CreateConnection())
            {
                int bookStatus=await connection.ExecuteAsync(query, new {request.RequestId,request.UserId,request.RoomId,request.Date,request.TimeSlot,request.Status});
                if(bookStatus > 0) {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Mohit :- Get User By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            string query = "select * from Users where Email=@Email";
            using(var connection=_dbContext.CreateConnection())
            {
                User user=await connection.QueryFirstOrDefaultAsync<User>(query, new { email });
                return user;
            }
        }

        /// <summary>
        /// Mohit :- Login User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<User> LoginUser(LoginUserCommand request)
        {
            string query = "select * from Users where Email=@Email and Password=@Password";
            using(var connection = _dbContext.CreateConnection())
            {
                User user = await connection.QueryFirstOrDefaultAsync<User>(query, new { request.Email,request.Password });
                return user;
            }

        }


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
    }
}
