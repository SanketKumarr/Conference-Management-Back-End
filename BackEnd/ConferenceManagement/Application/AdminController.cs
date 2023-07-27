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
        //Mohit
        [HttpGet]
        [Route("GetAllUser")]
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
        //Mohit
        [HttpDelete]
        [Route("DeleteRequest/{bookId:int}")]
        public async Task<ActionResult> DeleteRequestByBookId(int bookId)
        {
            bool deleteStatus= await _mediator.Send(new DeleteRequestCommand() { BookId = bookId });
            return Ok(deleteStatus);
        }
    }
}
