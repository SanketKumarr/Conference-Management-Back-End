using ConferenceManagement.Model;

namespace ConferenceManagement.Business.AdminDataAccess
{
    public interface IAdminDataAccess
    {
        //Get User By Id
        Task<User> GetUserById(int user_Id);

        //Delete User
        Task<bool> DeleteUser(int user_Id);
    }
}
