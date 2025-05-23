using CarService.Entities;
using CarService.Entities.Dto_s;

namespace CarService.DataAccess.Abstract
{
    public interface IAddUserToAdmin
    {
        Task<int> GetAddmin(int userId);
        Task AddToDatabase(AdminChatUsers chatUsers);
        Task DeleteUserFromChat(int userId);
        Task<List<ReturnUserDto>> GetUserList(int id)
        Task<Admin> GetAdmin(int id);
    }
}
