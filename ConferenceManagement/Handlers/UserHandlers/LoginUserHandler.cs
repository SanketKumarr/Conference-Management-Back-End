using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class LoginUserHandler:IRequestHandler<LoginUserCommand ,User>
    {
        private readonly IUserDataAccess _userDataAccess;
        public LoginUserHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userDataAccess.GetUserByEmail(request.Email);
            if (user != null)
            {
                User userlogin = await _userDataAccess.LoginUser(request);
                return userlogin;
            }
            return null;

        }
    }
}
