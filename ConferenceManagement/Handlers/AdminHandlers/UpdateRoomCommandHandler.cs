using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, bool>
    {
        private readonly IAdminDataAccess _adminDataAccess;

        public UpdateRoomCommandHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<bool> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            ConferenceRoom conferenceRoom = await _adminDataAccess.GetRoomById(request.RoomId);
            if (conferenceRoom != null)
            {
                conferenceRoom.RoomName = request.RoomName;
                conferenceRoom.Capacity = request.Capacity;
                conferenceRoom.IsAVRoom = request.IsAVRoom;
                conferenceRoom.Image = request.Image;

                return await _adminDataAccess.UpdateRoom(conferenceRoom);
            }
            else
            {
                throw new DataNotFoundException($"room id {request.RoomId} is not present ");
            }
            
        }
    }
}
