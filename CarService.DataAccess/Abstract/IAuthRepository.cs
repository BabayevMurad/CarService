using CarService.Entities;

namespace CarService.DataAccess.Abstract
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<Admin> AdminLogin(string username, string password);
        Task<Admin> AdminRegister(Admin admin, string password);
    }
}
