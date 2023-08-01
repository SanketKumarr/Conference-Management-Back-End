using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class CancleRoomCommandHandler : IRequestHandler<CancleRoomCommand, bool>
    {
        private readonly IUserDataAccess _userDataAccess;

        public CancleRoomCommandHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<bool> Handle(CancleRoomCommand request, CancellationToken cancellationToken)
        {
            BookRoom CheckbookRoom = await _userDataAccess.GetRoomByBookingId(request.BookingId);
            if (CheckbookRoom == null)
            {
                return false;
            }

            CheckbookRoom.Status = request.Status;

            return await _userDataAccess.CancleRoom(CheckbookRoom);
        }
    }
}
