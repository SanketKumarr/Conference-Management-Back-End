using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

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
            try
            {
                User user = await _mediator.Send(new LoginUserCommand() { Email = loginUser.Email, Password = loginUser.Password });
                if (user != null)
                {
                    string userToken = await _mediator.Send(new TokenGenerateCommand() { UserId = user.User_Id, Name = user.Name });
                  
                   // string newtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    HttpContext.Session.SetString("Token", userToken);                   
                    //var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken);
                    string userJwtSecurityToken = JsonConvert.SerializeObject(new { Token = userToken, Email = loginUser.Email });

                    //return userJwtSecurityTokenHandler;
                    return Ok(userJwtSecurityToken);
                }
                else
                {
                    return null;
                }
            }
            catch (UserNullException une)
            {
                return StatusCode(500,une.Message);
            }
            

        }

    }
}
