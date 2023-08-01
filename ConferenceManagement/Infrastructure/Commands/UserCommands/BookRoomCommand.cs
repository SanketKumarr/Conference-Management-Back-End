using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.UserCommands
{
    public class BookRoomCommand : IRequest<bool>
    {

        public string RequestId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string Date { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; }
        public BookRoomCommand(string requestId, int userId, int roomId, string date, string timeSlot, string status)
        {
            RequestId = requestId;
            UserId = userId;
            RoomId = roomId;
            Date = date;
            TimeSlot = timeSlot;
            Status = status;
        }
    }
}
