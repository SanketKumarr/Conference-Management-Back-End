using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class GetAllPendingRequestHandler : IRequestHandler<GetAllRequestByStatus, List<BookRoom>>
    {
        private readonly IAdminDataAccess _adminDataAccess;
        public GetAllPendingRequestHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<List<BookRoom>> Handle(GetAllRequestByStatus request, CancellationToken cancellationToken)
        {
            List<BookRoom> getPendingRequest= await _adminDataAccess.GetAllPendingRequest(request.Status);
            if (getPendingRequest != null)
            {
                return getPendingRequest;
            }
            else
            {
                throw new RequestNotFoundException($"Not found any pending Request ");
            }
        }
    }
}
