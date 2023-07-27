using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;

namespace ConferenceManagement.Business.UserDataAccess
{
    public interface IUserDataAccess
    {
        Task<bool> BookRoom(BookRoomCommand request);
        Task<User> GetUserByEmail(string email);
        Task<User> LoginUser(LoginUserCommand request);
    }
}
