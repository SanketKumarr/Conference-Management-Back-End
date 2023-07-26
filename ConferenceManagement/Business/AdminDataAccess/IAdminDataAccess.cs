using ConferenceManagement.Model;

namespace ConferenceManagement.Business.AdminDataAccess
{
    public interface IAdminDataAccess
    {
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
    }
}
