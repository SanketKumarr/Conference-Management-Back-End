using ConferenceManagement.Infrastructure.Commands.AdminCommands;

namespace ConferenceManagement.Business.Token
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(int userId, string name);
       
    }
}
