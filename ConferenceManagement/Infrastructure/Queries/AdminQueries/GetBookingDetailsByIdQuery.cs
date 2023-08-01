using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.AdminQueries
{
    public class GetBookingDetailsByIdQuery:IRequest<BookRoom>
    {
        public int BookingId { get; set; }
    }
}
