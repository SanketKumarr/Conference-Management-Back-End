﻿using ConferenceManagement.Infrastructure.Commands.UserCommands;
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

        //Ashish
        //Add User
        #region Add User
        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            bool AddUserStatus = await _mediator.Send(new AddUserCommand(user.Name, user.Email, user.Password, user.Designation));
            return Ok(AddUserStatus);
        }
        #endregion

        //Ashish
        //Edit User
        [Authorize]
        [Route("EditUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int user_Id, User user)
        {
            bool UpdateEmployeeStatus = await _mediator.Send(new UpdateUserCommand(user_Id, user.Name, user.Email, user.Password, user.Designation));
            return Ok(UpdateEmployeeStatus);
        }

        //Ashish
        //Display All Room
        [Route("DisplayAllRoom")]
        [HttpGet]
        public async Task<IActionResult> DisplayAllRoom()
        {
            List<ConferenceRoom> AllConferenceRoom = await _mediator.Send(new DisplayAllRoomQuery());
            return Ok(AllConferenceRoom);
        }
    }
}
