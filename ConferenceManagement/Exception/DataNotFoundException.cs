namespace ConferenceManagement.Exception
{
    public class DataNotFoundException:ApplicationException
    {
        public DataNotFoundException()
        {

        }
        public DataNotFoundException(string msg):base(msg) 
        {

        }
    }
}
