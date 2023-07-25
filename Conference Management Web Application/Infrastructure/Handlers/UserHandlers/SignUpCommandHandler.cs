using Conference_Management_Web_Application.Business.Interfaces;

namespace Conference_Management_Web_Application.Infrastructure.Handlers.UserHandlers
{
    public class SignUpCommandHandler
    {
        private readonly IBLL _iBLL;

        public SignUpCommandHandler(IBLL iBLL)
        {
            _iBLL = iBLL;
        }
    }
}
