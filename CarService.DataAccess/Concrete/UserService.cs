using CarService.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CarService.DataAccess.Concrete
{
    public class UserService : IUserService
    {
        private readonly AppDataContext _context;

        public UserService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetUserMoney(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user is not null)
            {
                return user.Money;
            }
            return -1;
        }

        public async Task<decimal> AddUserMoney(int userId, decimal money)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is not null)
            {
                user.Money = user.Money + money;

                _context.Users.Update(user);

                await _context.SaveChangesAsync();
            }

            return user.Money;
        }

        public async Task<decimal> UserPay(int userId, decimal money)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is not null)
            {
                if (user.Money >= money)
                {
                    user.Money = user.Money - money;

                    _context.Users.Update(user);

                    await _context.SaveChangesAsync();
                }
            }

            return user.Money;
        }
    }
}
