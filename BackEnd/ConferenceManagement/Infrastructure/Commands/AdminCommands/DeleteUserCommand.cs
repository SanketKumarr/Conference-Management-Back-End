using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.AdminCommands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int User_Id { get; set; }
    }
}
