using ConferenceManagement.Business.Token;
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

        /// <summary>
        /// Mohit :- Get All User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUser")]
        [Authorize]
        public async Task<ActionResult> GetAllUser()
        {
            
            List<User> allUsser = await _mediator.Send(new GetAllUserCommand());
            return Ok(allUsser);
        }

        /// <summary>
        /// Mohit :- Delete Request By Book Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRequest/{bookId:int}")]
        public async Task<ActionResult> DeleteRequestByBookId(int bookId)
        {
            bool deleteStatus= await _mediator.Send(new DeleteRequestCommand() { BookId = bookId });
            return Ok(deleteStatus);
        }

        /// <summary>
        /// Ashish :- Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Delete User
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool DeleteUserStatus = await _mediator.Send(new DeleteUserCommand() { User_Id = id });
            return Ok(DeleteUserStatus);
        }
        #endregion


        /// <summary>
        /// Ashish :- Add Room
        /// </summary>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>
        #region Add Room
        [Route("AddRoom")]
        [HttpPost]
        public async Task<IActionResult> AddRoom(ConferenceRoom conferenceRoom)
        {
            bool ConferenceRoomStatus = await _mediator.Send(new AddRoomCommand(conferenceRoom.RoomName, conferenceRoom.Capacity, conferenceRoom.IsAVRoom, conferenceRoom.Image));
            return Ok(ConferenceRoomStatus);
        }
        #endregion


        /// <summary>
        /// Ashish :- Update Room
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="conferenceRoom"></param>
        /// <returns></returns>
        #region Update Room
        [Route("UpdateRoom")]
        [HttpPut]
        public async Task<IActionResult> UpdateRoom(int roomId, ConferenceRoom conferenceRoom)
        {
            bool UpdateRoomStatus = await _mediator.Send(new UpdateRoomCommand(roomId, conferenceRoom.RoomName, conferenceRoom.Capacity, conferenceRoom.IsAVRoom, conferenceRoom.Image));
            return Ok(UpdateRoomStatus);
        }
        #endregion


        /// <summary> 
        /// Ashish :- Delete Room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        #region Delete Room
        [Route("DeleteRoom")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            bool DeleteUserStatus = await _mediator.Send(new DeleteRoomCommand() { RoomId = roomId });
            return Ok(DeleteUserStatus);
        }
        #endregion
    }
}
