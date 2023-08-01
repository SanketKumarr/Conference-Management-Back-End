using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Infrastructure.Queries.UserQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class RoomNotificationQueryHandler : IRequestHandler<RoomNotificationQuery, List<BookRoom>>
    {
        private readonly IUserDataAccess _userDataAccess;

        public RoomNotificationQueryHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }


        public async Task<List<BookRoom>> Handle(RoomNotificationQuery request, CancellationToken cancellationToken)
        {
            return await _userDataAccess.RoomNotification(request.UserId);
        }
    }
}
