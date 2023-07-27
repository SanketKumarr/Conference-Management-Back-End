using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.AdminCommands
{
    public class TokenGenerateCommand:IRequest<string>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
