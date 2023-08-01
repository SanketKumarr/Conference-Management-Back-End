using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
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
            BookRoom getRequest= await _adminDataAccess.GetRequestByBookId(request.BookingId);
            if (getRequest!=null)
            {
                bool deleteStatus = await _adminDataAccess.DeleteRequestByBookId(request.BookingId);
                return deleteStatus;
            }
            else
            {
                throw new RequestNotFoundException($" Id {request.BookingId} Does Not Exist");
            }
            
           
        }
    }
}
