using ConferenceManagement.Infrastructure.Commands.AdminCommands;

namespace ConferenceManagement.Business.Token
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(int userId, string name);
        Task<bool> IsTokenValid(string userSecretKey, string userIssuer, string userAudience, string userToken);
    }
}
