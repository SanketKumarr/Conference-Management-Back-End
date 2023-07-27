﻿using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConferenceManagement.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Mohit :- Login
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("loginUser")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {
            User user = await _mediator.Send(new LoginUserCommand() { Email = loginUser.Email, Password = loginUser.Password });
            if ( user!=null)
            {
                string userToken = await _mediator.Send(new TokenGenerateCommand() { UserId = user.User_Id, Name = user.Name });
                HttpContext.Session.SetString("Token", userToken);
                //var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken);
                string userJwtSecurityToken = JsonConvert.SerializeObject(new { Token = userToken });

                //return userJwtSecurityTokenHandler;
                return Ok(userJwtSecurityToken);
            }
            return null;

        }

    }
}
