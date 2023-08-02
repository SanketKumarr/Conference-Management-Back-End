using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class AddContactCommandHandler : IRequestHandler<AddContactCommand, bool>
    {
        private readonly IUserDataAccess _userDataAccess;



        public AddContactCommandHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }



        public async Task<bool> Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            Contact Checkcontact = await _userDataAccess.CheckContactByEmail(request.Email);
            if (Checkcontact == null)
            {
                Contact contact = new Contact()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Message = request.Message,
                };
                return await _userDataAccess.AddContact(contact);
            }
            else
            {
                throw new UserFoundException($"{request.Email}  is Already Present for this Contact ");
            }
        
        }
    }
}
