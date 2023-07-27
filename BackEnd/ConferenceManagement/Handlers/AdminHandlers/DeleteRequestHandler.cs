using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class DeleteRequestHandler:IRequestHandler<DeleteRequestCommand,bool>
    {
        private readonly IAdminDataAccess _adminDataAccess;
        public DeleteRequestHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<bool> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            return await _adminDataAccess.DeleteRequestByBookId(request.BookId);
        }
    }
}
