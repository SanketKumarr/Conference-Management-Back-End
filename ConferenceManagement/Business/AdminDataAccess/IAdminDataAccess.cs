using ConferenceManagement.Model;

namespace ConferenceManagement.Business.AdminDataAccess
{
    public interface IAdminDataAccess
    {
        //Delete Request By Book Id
        Task<bool> DeleteRequestByBookId(int bookId);


        //Get All User
        Task<List<User>> GetAllUser();


        //Delete User
        Task<bool> DeleteUser(int user_Id);


        //Get User By Id
        Task<User> GetUserById(int user_Id);


        //Add Room
        Task<bool> AddRoom(ConferenceRoom conferenceRoom);


        //Get Room By Id
        Task<ConferenceRoom> GetRoomById(int roomId);


        //Update Room
        Task<bool> UpdateRoom(ConferenceRoom conferenceRoom);


        //Delete Room
        Task<bool> DeleteRoom(int roomId);


        //Get All Pending Request
        Task<List<BookRoom>> GetAllPendingRequest(string status);


        //Get Room By Room Id
        Task<ConferenceRoom> GetRoomByRoomId(int roomId);


        //Get Booking Details By Id
        Task<BookRoom> GetBookingDetailsById(int bookingId);


        //Get All Bookings
        Task<List<BookRoom>> GetAllBookings();


        //Get Request By Book Id
        Task<BookRoom> GetRequestByBookId(int bookId);


        //Get All Contact
        Task<List<Contact>> GetAllContact();

        //Get All Notification
        Task<List<Notification>> GetAllNotification();
    }
}
