using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, bool>
    {
        private readonly IAdminDataAccess _adminDataAccess;

        public AddRoomCommandHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<bool> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            ConferenceRoom CheckConferenceRoom = await _adminDataAccess.GetRoomByName(request.RoomName);

            if (CheckConferenceRoom == null)
            {
                ConferenceRoom conferenceRoom = new ConferenceRoom()
                {
                    RoomName = request.RoomName,
                    Capacity = request.Capacity,
                    IsAVRoom = request.IsAVRoom,
                    Image = request.Image,
                };

                return await _adminDataAccess.AddRoom(conferenceRoom);
            }
            else
            {
                throw new RoomIdNotFoundException($"{request.RoomName} Room is Present");
            }
        }
    }
}
