using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IUserDataAccess _userDataAccess;

        public AddUserCommandHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User CheckUser = await _userDataAccess.GetUserByEmail(request.Email);
            if (CheckUser == null)
            {
                User user = new User()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = request.Password,
                    Designation = request.Designation,
                };

                return await _userDataAccess.AddUser(user);
            }
            else
            {
                throw new UserFoundException($"{request.Email} of User is Already Present ");
            }
        }
    }
}
