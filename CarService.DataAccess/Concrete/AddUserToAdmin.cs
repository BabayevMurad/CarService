using CarService.DataAccess.Abstract;
using CarService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarService.DataAccess.Concrete
{
    public class AddUserToAdmin : IAddUserToAdmin
    {
        private readonly AppDataContext _context;

        public AddUserToAdmin(AppDataContext context)
        {
            _context = context;
        }

        public async Task<int> GetAddmin(int userId)
        {
            var admins = await _context.Admins.ToListAsync();

            var userAdminChatList = await _context.AdminChatUsers.ToListAsync();

            var userToAdd = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            Dictionary<int, int> sub = new Dictionary<int, int>();

            for (int i = 0; i < admins.Count; i++)
            {
                int count = 0;

                for (int j = 0; j < userAdminChatList.Count; j++)
                {
                    if (userAdminChatList[j].UserId == admins[i].Id)
                    {
                        count++;
                    }
                }

                sub.Add(admins[i].Id, count);
            }

            var minCount = 0;
            var minCountAdminId = -1;


            foreach (var admin in sub)
            {
                if (admin.Value < minCount)
                {
                    minCount = admin.Value;
                    minCountAdminId = admin.Key;
                }
            }

            if(minCountAdminId == -1)
            {
                minCountAdminId = _context.Admins.FirstAsync().Id;
            }

            return minCountAdminId;
        }

        public async Task AddToDatabase(AdminChatUsers chatUsers)
        {
            _context.AdminChatUsers.Add(chatUsers);

            await _context.SaveChangesAsync();

        }

        public async Task DeleteUserFromChat(int userId)
        {
            var userToAdd = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var userAdminChatList = await _context.AdminChatUsers.ToListAsync();

            var userToDelete = userAdminChatList.FirstOrDefault(x => x.UserId == userId);

            _context.AdminChatUsers.Remove(userToDelete!);

            await _context.SaveChangesAsync();
        }
    }
}
