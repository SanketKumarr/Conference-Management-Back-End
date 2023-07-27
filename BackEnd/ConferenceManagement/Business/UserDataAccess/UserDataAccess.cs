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

        public async Task<User> GetUserByEmail(string email)
        {
            string query = "select * from Users where Email=@Email";
            using(var connection=_dbContext.CreateConnection())
            {
                User user=await connection.QueryFirstOrDefaultAsync<User>(query, new { email });
                return user;
            }
        }

        public async Task<User> LoginUser(LoginUserCommand request)
        {
            string query = "select * from Users where Email=@Email and Password=@Password";
            using(var connection = _dbContext.CreateConnection())
            {
                User user = await connection.QueryFirstOrDefaultAsync<User>(query, new { request.Email,request.Password });
                return user;
            }

        }
    }
}
