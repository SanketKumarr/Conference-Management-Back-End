using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class GetBookingDetailsByIdHandler : IRequestHandler<GetBookingDetailsByIdQuery, BookRoom>
    {
        private readonly IAdminDataAccess _adminDataAccess;
        public GetBookingDetailsByIdHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess= adminDataAccess;
        }
        public async Task<BookRoom> Handle(GetBookingDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            BookRoom bookRoomDetails= await _adminDataAccess.GetBookingDetailsById(request.BookingId);
            if (bookRoomDetails != null) 
            {
                return bookRoomDetails;
            }
            else
            {
                throw new DataNotFoundException($"Booking id {request.BookingId} is not found");
            }
        }
    }
}
