namespace ConferenceManagement.Exception
{
    public class UserNotFoundException:ApplicationException   
    {

        public UserNotFoundException(string msg) : base(msg)
        {
            
        }
    }
}
