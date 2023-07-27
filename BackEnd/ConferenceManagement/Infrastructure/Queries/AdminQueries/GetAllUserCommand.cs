using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.AdminQueries
{
    public class GetAllUserCommand:IRequest<List<User>>
    {

    }
}
