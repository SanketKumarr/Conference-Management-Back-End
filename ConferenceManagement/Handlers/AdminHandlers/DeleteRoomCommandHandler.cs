using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
    {
        private readonly IAdminDataAccess _adminDataAccess;

        public DeleteRoomCommandHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            ConferenceRoom conferenceRoom = await _adminDataAccess.GetRoomById(request.RoomId);
            if (conferenceRoom != null)
            {
                return await _adminDataAccess.DeleteRoom(request.RoomId);
            }
            else
            {
                throw new DataNotFoundException($"room id {request.RoomId} is not present");
            }
            
        }
    }
}
