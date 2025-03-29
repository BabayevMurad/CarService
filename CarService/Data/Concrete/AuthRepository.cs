using CarService.Data.Abstract;
using CarService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CarService.Data.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDataContext _context;

        public AuthRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null!;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null!;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[]? passwordHash, byte[]? passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt!))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash!);
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            var hasExist = await _context
                .Users
                .AnyAsync(u => u.Username == username);

            return hasExist;
        }
    }
}
