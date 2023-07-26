using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;

namespace ConferenceManagement.Business.UserDataAccess
{
    public interface IUserDataAccess
    {
        Task<bool> AddUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int user_Id);
        Task<User> LoginUser(LoginUserCommand request);
        Task<bool> UpdateUser(User user);
    }
}
