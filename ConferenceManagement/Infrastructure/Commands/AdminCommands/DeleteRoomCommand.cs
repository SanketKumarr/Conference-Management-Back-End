using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.AdminCommands
{
    public class DeleteRoomCommand : IRequest<bool>
    {
        public int RoomId { get; set; }
    }
}
