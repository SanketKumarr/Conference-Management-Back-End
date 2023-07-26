using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.UserQueries
{
    public class DisplayAllRoomQuery : IRequest<List<ConferenceRoom>>
    {
    }
}
