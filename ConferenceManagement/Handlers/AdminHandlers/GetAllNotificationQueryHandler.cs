using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class GetAllNotificationQueryHandler : IRequestHandler<GetAllNotificationQuery, List<Notification>>
    {
        private readonly IAdminDataAccess _adminDataAccess;

        public GetAllNotificationQueryHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<List<Notification>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            return await _adminDataAccess.GetAllNotification();
        }
    }
}
