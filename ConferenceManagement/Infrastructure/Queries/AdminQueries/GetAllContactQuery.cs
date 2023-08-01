using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.AdminQueries
{
    public class GetAllContactQuery : IRequest<List<Contact>>
    {
    }
}
