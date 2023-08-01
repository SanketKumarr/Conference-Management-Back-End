using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.AdminQueries
{
    public class GetRoomByRoomIdQueries:IRequest<ConferenceRoom>
    {
        public int RoomId { get; set; }
    }
}
