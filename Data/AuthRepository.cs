using System;
using System.Threading.Tasks;
using angu.models;
using Microsoft.EntityFrameworkCore;

namespace angu.Data
{
    public class AuthRepository : IAuthRespository  
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());

            if (user == null) return null;

            if(!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
         using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] == passwordHash[i]) return false;
                }
            }   

            return true;
        }

        public async Task<User> RegisterUser(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
           return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExist(string username)
        {
            if(await _context.Users.AnyAsync(x=> x.Username == username)) return true;
            return false;
        }
    }
}