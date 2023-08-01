using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.UserQueries
{
    public class RoomNotificationQuery : IRequest<List<BookRoom>>
    {
        public int UserId { get; set; }
    }
}
