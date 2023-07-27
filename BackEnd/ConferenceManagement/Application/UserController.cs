using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagement.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //Mohit 
        [HttpPost]
        [Route("BookRoom")]
        public async Task<ActionResult> BookRoom(BookRoom bookRoom)
        {
            bool bookStatus = await _mediator.Send(new BookRoomCommand(bookRoom.RequestId, bookRoom.UserId, bookRoom.RoomId, bookRoom.Date, bookRoom.TimeSlot, bookRoom.Status));
            
            return Ok(bookStatus);
        }
    }
}
