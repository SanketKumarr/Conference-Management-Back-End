using MediatR;

namespace ConferenceManagement.Infrastructure.Commands.UserCommands
{
    public class AddUserCommand : IRequest<bool>
    {
        public AddUserCommand(string name, string email, string password, string designation)
        {
            Name = name;
            Email = email;
            Password = password;
            Designation = designation;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
    }
}
