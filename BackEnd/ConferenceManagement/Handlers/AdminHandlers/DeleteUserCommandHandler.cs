using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IAdminDataAccess _adminDataAccess;

        public DeleteUserCommandHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _adminDataAccess.GetUserById(request.User_Id);
            if (user == null)
            {
                return false;
            }
            return await _adminDataAccess.DeleteUser(request.User_Id);
        }
    }
}
