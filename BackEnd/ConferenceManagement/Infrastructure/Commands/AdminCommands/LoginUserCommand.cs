using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.AdminCommands
{
    public class LoginUserCommand:IRequest<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
