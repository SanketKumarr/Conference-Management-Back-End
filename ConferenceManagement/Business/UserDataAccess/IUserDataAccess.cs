using ConferenceManagement.Infrastructure.Commands.AdminCommands;
using ConferenceManagement.Infrastructure.Commands.UserCommands;
using ConferenceManagement.Model;

namespace ConferenceManagement.Business.UserDataAccess
{
    public interface IUserDataAccess
    {
        //Book Room
        Task<bool> BookRoom(BookRoomCommand request);

        //Get User By Email
        Task<User> GetUserByEmail(string email);

        //Login User
        Task<User> LoginUser(LoginUserCommand request);

        //Add User
        Task<bool> AddUser(User user);

        //Display All Room
        Task<List<ConferenceRoom>> DisplayAllRoom();

        //Get User By Id
        Task<User> GetUserById(int user_Id);

        //Update User
        Task<bool> UpdateUser(User user);

        //Get Room By Room Id
        Task<ConferenceRoom> GetRoomByRoomId(int roomId);

        //Get Room By Booking Id
        Task<BookRoom> GetRoomByBookingId(int bookingId);

        //Cancle Room
        Task<bool> CancleRoom(BookRoom checkbookRoom);

        //Check Contact By Email
        Task<Contact> CheckContactByEmail(string email);

        //Add Contact
        Task<bool> AddContact(Contact contact);
    }
}
