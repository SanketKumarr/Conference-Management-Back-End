using ConferenceManagement.Business.Token;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public AdminController(IMediator mediator, IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _mediator = mediator;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        #region GetAllUser
        [Route("GetAllUser")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllUser()
        {
            string userToken = HttpContext.Session.GetString("Token");
            if (userToken != null)
            {
                var userSecretKey = _configuration["JwtValidationParameters:UserSecretKey"];
                var userIssuer = _configuration["JwtValidationParameters:UserIssuer"];
                var userAudience = _configuration["JwtValidationParameters:UserAudience"];
                bool isTokenValid = await _tokenGenerator.IsTokenValid(userSecretKey, userIssuer, userAudience, userToken);
                if (isTokenValid)
                {
                    List<User> allUsser = await _mediator.Send(new GetAllUserCommand());
                    return Ok(allUsser);
                }
            }
            return null;


        }
        #endregion


        #region Delete User
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool DeleteUserStatus = await _mediator.Send(new DeleteUserCommand() { User_Id = id });
            return Ok(DeleteUserStatus);
        }
        #endregion


        #region Add Room
        [Route("AddRoom")]
        [HttpPost]
        public async Task<IActionResult> AddRoom(ConferenceRoom conferenceRoom)
        {
            bool ConferenceRoomStatus = await _mediator.Send(new AddRoomCommand(conferenceRoom.RoomName, conferenceRoom.Capacity, conferenceRoom.IsAVRoom, conferenceRoom.Image));
            return Ok(ConferenceRoomStatus);
        }
        #endregion

        #region Update Room
        [Route("UpdateRoom")]
        [HttpPut]
        public async Task<IActionResult> UpdateRoom(int roomId, ConferenceRoom conferenceRoom)
        {
            bool UpdateRoomStatus = await _mediator.Send(new UpdateRoomCommand(roomId, conferenceRoom.RoomName, conferenceRoom.Capacity, conferenceRoom.IsAVRoom, conferenceRoom.Image));
            return Ok(UpdateRoomStatus);
        }
        #endregion

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
