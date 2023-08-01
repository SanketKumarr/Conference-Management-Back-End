using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class GetAllContactQueryHandler : IRequestHandler<GetAllContactQuery, List<Contact>>
    {
        private readonly IAdminDataAccess _adminDataAccess;

        public GetAllContactQueryHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<List<Contact>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            return await _adminDataAccess.GetAllContact();
        }
    }
}
