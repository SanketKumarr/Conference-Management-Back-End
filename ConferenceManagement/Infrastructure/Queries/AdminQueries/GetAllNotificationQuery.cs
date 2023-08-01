using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.AdminQueries
{
    public class GetAllNotificationQuery : IRequest<List<Notification>>
    {
    }
}
