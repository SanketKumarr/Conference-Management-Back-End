using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.AdminCommands
{
    public class UpdateRoomCommand : IRequest<bool>
    {
        public UpdateRoomCommand(int roomId, string roomName, int capacity, bool isAVRoom, string image)
        {
            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
            IsAVRoom = isAVRoom;
            Image = image;
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public bool IsAVRoom { get; set; }
        public string Image { get; set; }
    }
}
