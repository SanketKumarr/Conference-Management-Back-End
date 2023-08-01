using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Exception;
using ConferenceManagement.Infrastructure.Queries.AdminQueries;
using ConferenceManagement.Model;
using MediatR;

namespace ConferenceManagement.Handlers.AdminHandlers
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserCommand, List<User>>
    {
        private readonly IAdminDataAccess _adminDataAccess;
        public GetAllUserHandler(IAdminDataAccess adminDataAccess)
        {
            _adminDataAccess = adminDataAccess;
        }

        public async Task<List<User>> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            List<User> allUser= await _adminDataAccess.GetAllUser();
            if(allUser!=null) 
            { 
                return allUser;
            }
            else
            {
                throw new UserNullException($"Null User");
            }
        }
    }
}
