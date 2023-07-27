﻿using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.UserHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserDataAccess _userDataAccess;

        public UpdateUserCommandHandler(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userDataAccess.GetUserById(request.User_Id);
            if (user == null)
            {
                return false;
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Designation = request.Designation;

            return await _userDataAccess.UpdateUser(user);
        }
    }
}
