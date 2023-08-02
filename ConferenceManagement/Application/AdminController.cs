using ConferenceManagement.Business.Token;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagement.Application
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;
        public AdminController(IMediator mediator,IConfiguration configuration,ITokenGenerator tokenGenerator)
        {
            _mediator = mediator;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }



        #region Get All User
        /// <summary>
        /// Mohit :- Get All User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUser")]
        //[Authorize]
        public async Task<ActionResult> GetAllUser()
        {
            try
            {
                List<User> allUsser = await _mediator.Send(new GetAllUserCommand());
                return Ok(allUsser);
            }
            catch (UserNullException une)
            {
                return StatusCode(500, une.Message);
            }

        }
        #endregion

        
        #region DeleteRequest By Book Id
        /// <summary>
        /// Mohit :- Delete Request By Book Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRequest/{bookId:int}")]
        public async Task<ActionResult> DeleteRequestByBookId(int bookId)
        {
            try
            {
                bool deleteStatus = await _mediator.Send(new DeleteRequestCommand() { BookingId = bookId });
                return Ok(deleteStatus);
            }
            catch (RequestNotFoundException rnfe)
            {
                return StatusCode(500, rnfe.Message);
            }
        }
        #endregion


        #region Delete User
        /// <summary>
        /// Ashish :- Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                bool DeleteUserStatus = await _mediator.Send(new DeleteUserCommand() { User_Id = id });
                return Ok(DeleteUserStatus);
            }
            catch (UserNullException une)
            {
                return StatusCode(500,une.Message);
            }
        }
        #endregion


        #region Add Room
        /// <summary>
        /// Ashish :- Add Room
        /// </summary>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>
        [Route("AddRoom")]
        [HttpPost]
        public async Task<IActionResult> AddRoom(ConferenceRoom conferenceRoom)
        {
            try
            {
                bool ConferenceRoomStatus = await _mediator.Send(new AddRoomCommand(conferenceRoom.RoomName, conferenceRoom.Capacity, conferenceRoom.IsAVRoom, conferenceRoom.Image));
                return Ok(ConferenceRoomStatus);
            }
            catch (RoomIdNotFoundException une)
            {
                return StatusCode(500, une.Message);
            }
        }
        #endregion


        #region Update Room
        /// <summary>
        /// Ashish :- Update Room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>
        [Route("UpdateRoom/{roomId:int}")]
        [HttpPut]
        public async Task<IActionResult> UpdateRoom(int roomId, ConferenceRoom conferenceRoom)
        {
            try
            {
                bool UpdateRoomStatus = await _mediator.Send(new UpdateRoomCommand(roomId, conferenceRoom.RoomName, conferenceRoom.Capacity, conferenceRoom.IsAVRoom, conferenceRoom.Image));
                return Ok(UpdateRoomStatus);
            }
            catch (DataNotFoundException dnfe)
            {
                return StatusCode(500,dnfe.Message);
            }
           
        }
        #endregion


        #region Delete Room
        /// <summary> 
        /// Ashish :- Delete Room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>

        [Route("DeleteRoom/{roomId:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            try
            {
                bool DeleteUserStatus = await _mediator.Send(new DeleteRoomCommand() { RoomId = roomId });
                return Ok(DeleteUserStatus);
            }
            catch (DataNotFoundException dnfe)
            {
                return StatusCode(500,dnfe.Message);
            }
        }
        #endregion


        #region Display Request By Status
        /// <summary>
        /// Mohit:- Display Request By Status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DisplayRequestByStatus/{status}")]
        public async Task<ActionResult> DisplayRequestByStatus(string status)
        {
            try
            {
                List<BookRoom> allPendingRequest = await _mediator.Send(new GetAllRequestByStatus() { Status = status });
                return Ok(allPendingRequest);
            }
            catch (RequestNotFoundException rnfe)
            {
                return StatusCode(500, rnfe.Message);
            }
        }
        #endregion


        #region Get Room By Room Id
        /// <summary>
        /// Mohit:- Get Room By Room Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoomById/{roomId}")]
        public async Task<ActionResult> GetRoomByRoomId(int roomId)
        {
            try
            {
                ConferenceRoom roomDetails = await _mediator.Send(new GetRoomByRoomIdQueries() { RoomId = roomId });
                return Ok(roomDetails);
            }
            catch (DataNotFoundException dnfe)
            {
                return StatusCode(500, dnfe.Message);
            }
        }
        #endregion


        #region Get All Booking
        /// <summary>
        /// Mansi :- Get All Booking
        /// </summary>
        /// <returns></returns>
        [Route("GetAllBooking")]
        [HttpGet]
        public async Task<ActionResult> GetAllBooking()
        {
            List<BookRoom> AllBookRooms = await _mediator.Send(new GetAllBookingsQuery());
            return Ok(AllBookRooms);
        }
        #endregion


        #region Get Room By Id
        /// <summary>
        /// Mansi :- Get Room By Id
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBookingDetails/{bookingId:int}")]
        public async Task<IActionResult> GetBookingDetailsById(int bookingId)
        {
            try
            {
                BookRoom CheckBookRoomById = await _mediator.Send(new GetBookingDetailsByIdQuery() { BookingId = bookingId });
                return Ok(CheckBookRoomById);
            }
            catch (DataNotFoundException dnfe)
            {
                return StatusCode(500, dnfe.Message);
            }

        }
        #endregion

        #region Get All Contact
        /// <summary>
        /// Sachin :- Get All Contact
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllContact")]
        public async Task<IActionResult> GetAllContact()
        {
            List<Contact> AllContact = await _mediator.Send(new GetAllContactQuery());
            return Ok(AllContact);
        }
        #endregion

        #region Get All Notification
        /// <summary>
        /// Mansi :- Get All Notification
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllNotification")]
        public async Task<IActionResult> GetAllNotification()
        {
            List<Notification> allnotification = await _mediator.Send(new GetAllNotificationQuery());
            return Ok(allnotification);
        }
        #endregion
    }
}
