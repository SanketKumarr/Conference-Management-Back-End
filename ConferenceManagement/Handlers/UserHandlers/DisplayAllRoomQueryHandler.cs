using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Infrastructure.Queries.UserQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class DisplayAllRoomQueryHandler : IRequestHandler<DisplayAllRoomQuery, List<ConferenceRoom>>
    {
        private readonly IUserDataAccess _userDataAccess;
        public DisplayAllRoomQueryHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<List<ConferenceRoom>> Handle(DisplayAllRoomQuery request, CancellationToken cancellationToken)
        {
            return await _userDataAccess.DisplayAllRoom();
        }
    }
}
