using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
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

        #region Book Room
        /// <summary>
        /// Mohit :- Book Room
        /// </summary>
        /// <param name="bookRoom"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        [Route("BookRoom")]
        public async Task<ActionResult> BookRoom(BookRoom bookRoom)
        {
            try
            {
                bookRoom.RequestId = Guid.NewGuid().ToString();
                bookRoom.Status = "Pending";
                bool bookStatus = await _mediator.Send(new BookRoomCommand(bookRoom.RequestId, bookRoom.UserId, bookRoom.RoomId, bookRoom.Date, bookRoom.TimeSlot, bookRoom.Status));
                return Ok(bookStatus);
            }
            catch (DataNotFoundException dnfe)
            {
                return StatusCode(500, dnfe.Message);
            }
        }
        #endregion


        #region Add User
        /// <summary>
        /// Ashish :- Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            bool AddUserStatus = await _mediator.Send(new AddUserCommand(user.Name, user.Email, user.Password, user.Designation));
            return Ok(AddUserStatus);
        }
        #endregion

        #region Update User
        /// <summary>
        /// Ashish :- Update User
        /// </summary>
        /// <param name="user_Id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("EditUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int user_Id, User user)
        {
            bool UpdateEmployeeStatus = await _mediator.Send(new UpdateUserCommand(user_Id, user.Name, user.Email, user.Password, user.Designation));
            return Ok(UpdateEmployeeStatus);
        }
        #endregion


        #region Display All Room
        /// <summary>
        /// Ashish :- Display All Room
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [Route("DisplayAllRoom")]
        [HttpGet]
        public async Task<IActionResult> DisplayAllRoom()
        {
            List<ConferenceRoom> AllConferenceRoom = await _mediator.Send(new DisplayAllRoomQuery());
            return Ok(AllConferenceRoom);
        }
        #endregion


        #region Get User By Email
        /// <summary>
        /// Mohit:- 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public async Task<ActionResult> GetUserByEmail(string email)
        {
            try
            {
                User user = await _mediator.Send(new GetUserByEmailCommand() { Email = email });
                return Ok(user);
            }
            catch (UserNullException une)
            {
                return StatusCode(500, une.Message);
            }
        }
        #endregion

        #region Cancle Room
        /// <summary>
        /// Mansi :- CancleRoom
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("CancleRoom")]
        public async Task<IActionResult> CancleRoom(int bookingId, string status)
        {
            bool CancleRoomStatus = await _mediator.Send(new CancleRoomCommand(bookingId, status));
            return Ok(CancleRoomStatus);
        }
        #endregion

        #region Add Contact
        /// <summary>
        /// Mansi :- Add Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddContact")]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            bool CheckAddContact = await _mediator.Send(new AddContactCommand(contact.Name, contact.Email, contact.Phone, contact.Message));
            return Ok(CheckAddContact);
        }
        #endregion

        /// <summary>
        /// Sachin :- Room Notification
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RoomNotification")]
        public async Task<IActionResult> RoomNotification(int userId)
        {
            List<BookRoom> AllbookRoomNotification = await _mediator.Send(new RoomNotificationQuery() { UserId = userId });
            return Ok(AllbookRoomNotification);
        }
    }
}
