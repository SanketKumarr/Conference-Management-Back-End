using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Infrastructure.Queries.UserQueries;
using ConferenceManagement.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Mohit :- Book Room
        /// </summary>
        /// <param name="bookRoom"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("BookRoom")]
        public async Task<ActionResult> BookRoom(BookRoom bookRoom)
        {
            bool bookStatus = await _mediator.Send(new BookRoomCommand(bookRoom.RequestId, bookRoom.UserId, bookRoom.RoomId, bookRoom.Date, bookRoom.TimeSlot, bookRoom.Status));
            
            return Ok(bookStatus);
        }


        /// <summary>
        /// Ashish :- Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        #region Add User
        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            bool AddUserStatus = await _mediator.Send(new AddUserCommand(user.Name, user.Email, user.Password, user.Designation));
            return Ok(AddUserStatus);
        }
        #endregion

        /// <summary>
        /// Ashish :- Update User
        /// </summary>
        /// <param name="user_Id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [Route("EditUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int user_Id, User user)
        {
            bool UpdateEmployeeStatus = await _mediator.Send(new UpdateUserCommand(user_Id, user.Name, user.Email, user.Password, user.Designation));
            return Ok(UpdateEmployeeStatus);
        }


        /// <summary>
        /// Ashish :- Display All Room
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("DisplayAllRoom")]
        [HttpGet]
        public async Task<IActionResult> DisplayAllRoom()
        {
            List<ConferenceRoom> AllConferenceRoom = await _mediator.Send(new DisplayAllRoomQuery());
            return Ok(AllConferenceRoom);
        }
    }
}
