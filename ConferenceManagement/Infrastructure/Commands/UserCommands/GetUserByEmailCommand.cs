using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.UserCommands
{
    public class GetUserByEmailCommand:IRequest<User>
    {
        public string Email { get; set; }
    }
}
