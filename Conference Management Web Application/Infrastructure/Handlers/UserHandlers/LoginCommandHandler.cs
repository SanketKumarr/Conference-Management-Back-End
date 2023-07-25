using Conference_Management_Web_Application.Business.Interfaces;

namespace Conference_Management_Web_Application.Infrastructure.Handlers.UserHandlers
{
    public class LoginCommandHandler
    {
        private readonly IBLL _iBLL;

        public LoginCommandHandler(IBLL iBLL)
        {
            _iBLL = iBLL;
        }
    }
}
