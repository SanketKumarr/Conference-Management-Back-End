namespace ConferenceManagement.Exception
{
    public class UserNullException:ApplicationException
    {
        public UserNullException()
        {

        }
        public UserNullException(string msg):base(msg) 
        {

        }
    }
}
