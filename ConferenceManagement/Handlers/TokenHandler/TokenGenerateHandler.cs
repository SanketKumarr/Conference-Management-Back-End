using ConferenceManagement.Business.Token;
using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using MediatR;

namespace ConferenceManagement.Handlers.TokenHandler
{
    public class TokenGenerateHandler : IRequestHandler<TokenGenerateCommand, string>
    {
        private readonly ITokenGenerator _tokenGenerator;
        public TokenGenerateHandler(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Handle(TokenGenerateCommand request, CancellationToken cancellationToken)
        {
            return await _tokenGenerator.GenerateToken(request.UserId,request.Name);
        }
    }
}
