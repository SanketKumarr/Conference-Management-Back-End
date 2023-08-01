namespace ConferenceManagement.Exception
{
    public class RequestNotFoundException:ApplicationException
    {
        public RequestNotFoundException()
        {

        }
        public RequestNotFoundException(string msg):base(msg) 
        {

        }
    }
}
