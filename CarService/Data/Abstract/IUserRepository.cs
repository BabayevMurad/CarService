namespace CarService.Data.Abstract
{
    public interface IUserRepository
    {
        public void AddUserMoney(int userId, decimal money); 
        public void UserPay(int userId, decimal money); 
    }
}
