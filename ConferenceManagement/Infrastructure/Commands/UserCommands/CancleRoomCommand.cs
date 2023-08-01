using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.UserCommands
{
    public class CancleRoomCommand : IRequest<bool>
    {
        public CancleRoomCommand(int bookingId, string status)
        {
            BookingId = bookingId;
            Status = status;
        }

        public int BookingId { get; set; }
        public string Status { get; set; }
    }
}
