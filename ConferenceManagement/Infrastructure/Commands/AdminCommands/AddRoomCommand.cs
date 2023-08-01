using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.AdminCommands
{
    public class AddRoomCommand : IRequest<bool>
    {
        public AddRoomCommand(string roomName, int capacity, bool isAVRoom, string image)
        {
            RoomName = roomName;
            Capacity = capacity;
            IsAVRoom = isAVRoom;
            Image = image;
        }

        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public bool IsAVRoom { get; set; }
        public string Image { get; set; }
    }
}
