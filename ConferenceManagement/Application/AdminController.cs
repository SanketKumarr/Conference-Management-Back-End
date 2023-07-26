using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagement.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Delete User
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool DeleteUserStatus = await _mediator.Send(new DeleteUserCommand() { User_Id = id });
            return Ok(DeleteUserStatus);
        }
    }
}
