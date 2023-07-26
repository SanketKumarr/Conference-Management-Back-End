using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;

namespace ConferenceManagement.Business.UserDataAccess
{
    public interface IUserDataAccess
    {
        //Add User
        
        Task<bool> AddUser(User user);

        //Get User By Id
        Task<User> GetUserById(int user_Id);

        //Update User
        Task<bool> UpdateUser(User user);
    }
}
