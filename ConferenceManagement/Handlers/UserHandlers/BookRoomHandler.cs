using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
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
            ConferenceRoom conferenceRoom= await _userDataAccess.GetRoomByRoomId(request.RoomId);
            if (conferenceRoom!=null)
            {
                return await _userDataAccess.BookRoom(request);
            }
            else
            {
                throw new DataNotFoundException($"Room id {request.RoomId} is not present ");
            }
        }
    }
}
