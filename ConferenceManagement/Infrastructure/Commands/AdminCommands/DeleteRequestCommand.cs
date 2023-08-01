using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.AdminCommands
{
    public class DeleteRequestCommand:IRequest<bool>
    {
        public int BookingId { get; set; }
    }
}
