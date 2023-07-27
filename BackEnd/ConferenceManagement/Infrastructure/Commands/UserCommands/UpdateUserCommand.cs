using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagement.Infrastructure.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UpdateUserCommand(int user_Id, string name, string email, string password, string designation)
        {
            User_Id = user_Id;
            Name = name;
            Email = email;
            Password = password;
            Designation = designation;
        }

        public int User_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
    }
}
