using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class BookRoomHandler : IRequestHandler<BookRoomCommand, bool>
    {
        private readonly IUserDataAccess _userDataAccess;
        public BookRoomHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<bool> Handle(BookRoomCommand request, CancellationToken cancellationToken)
        {
            return await _userDataAccess.BookRoom(request);
        }
    }
}
