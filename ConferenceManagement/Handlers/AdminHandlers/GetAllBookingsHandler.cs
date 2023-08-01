using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, List<BookRoom>>
    {
        private readonly IAdminDataAccess _adminDataAccess;
        public GetAllBookingsHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess= adminDataAccess;
        }

        public async Task<List<BookRoom>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _adminDataAccess.GetAllBookings();
        }
    }
}
