using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Queries.AdminQueries
{
    public class GetAllRequestByStatus:IRequest<List<BookRoom>>
    {
        public string Status { get; set; }
    }
}
