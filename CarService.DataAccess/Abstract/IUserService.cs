namespace CarService.DataAccess.Abstract
{
    public interface IUserService
    {
        public Task<decimal> AddUserMoney(int userId, decimal money);
        public Task<decimal> UserPay(int userId, decimal money);
        public Task<decimal> GetUserMoney(int userId);
    }
}
