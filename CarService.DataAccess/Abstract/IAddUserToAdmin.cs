using CarService.Entities;

namespace CarService.DataAccess.Abstract
{
    public interface IAddUserToAdmin
    {
        Task<int> GetAddmin(int userId);
        Task AddToDatabase(AdminChatUsers chatUsers);
        Task DeleteUserFromChat(int userId);
    }
}
