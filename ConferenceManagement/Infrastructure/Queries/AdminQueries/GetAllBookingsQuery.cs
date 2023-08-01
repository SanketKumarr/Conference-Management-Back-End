using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.AdminQueries
{
    public class GetAllBookingsQuery : IRequest<List<BookRoom>>
    {

    }
}
