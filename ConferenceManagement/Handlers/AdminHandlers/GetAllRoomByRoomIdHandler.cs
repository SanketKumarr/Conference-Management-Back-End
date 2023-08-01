using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class GetAllRoomByRoomIdHandler : IRequestHandler<GetRoomByRoomIdQueries, ConferenceRoom>
    {
        private readonly IAdminDataAccess _adminDataAccess;
        public GetAllRoomByRoomIdHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess= adminDataAccess;
        }
        public async Task<ConferenceRoom> Handle(GetRoomByRoomIdQueries request, CancellationToken cancellationToken)
        {
            ConferenceRoom getRoomDetails= await _adminDataAccess.GetRoomByRoomId(request.RoomId);
            if (getRoomDetails != null)
            {
                return getRoomDetails;
            }
            else
            {
                throw new DataNotFoundException($"Room id {request.RoomId} not found");
            }
        }
    }
}
