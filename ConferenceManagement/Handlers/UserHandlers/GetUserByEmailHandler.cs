using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailCommand, User>
    {
        private readonly IUserDataAccess _userDataAccess;
        public GetUserByEmailHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess= userDataAccess;
        }
        public async Task<User> Handle(GetUserByEmailCommand request, CancellationToken cancellationToken)
        {
            User userDetails=await _userDataAccess.GetUserByEmail(request.Email);
            if (userDetails != null)
            { 
                return userDetails;
            }
            else
            {
                throw new UserNullException($"User Not Found  ");
            }
        }
    }
}
