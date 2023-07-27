using ConferenceManagement.Model;

namespace ConferenceManagement.Business.AdminDataAccess
{
    public interface IAdminDataAccess
    {
        Task<bool> DeleteRequestByBookId(int bookId);
        Task<List<User>> GetAllUser();
    }
}
