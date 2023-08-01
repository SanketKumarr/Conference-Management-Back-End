using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.UserCommands
{
    public class AddContactCommand : IRequest<bool>
    {
        public AddContactCommand(string name, string email, string phone, string message)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Message = message;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
