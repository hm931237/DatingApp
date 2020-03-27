using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            this._context = context;
        }
        public async Task<User> Register (User user,string Password)
        {
            byte[] PasswordHash,PasswordSalt;
            CreatePasswordHash(Password,out PasswordHash,out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> Login (string username, string Password)
        {
            var User=await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if(User == null)
                return null;

            if(!VerifyPasswordHAsh(Password,User.PasswordHash,User.PasswordSalt))
                return null;

            return User;
        }
        public async Task<bool> UserExists (string username)
        {
            if(await _context.Users.AnyAsync(x => x.UserName == username))
                return true;

            return false;
        }
         
        private void CreatePasswordHash(string Password,out byte[] PasswordHash,out byte[] PasswordSalt)
        {
            using (var hmac =new System.Security.Cryptography.HMACSHA512())
            {
                  PasswordSalt = hmac.Key; 
                  PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        private bool VerifyPasswordHAsh(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac =new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                  var ComputeHash =hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                  for (int i = 0; i < ComputeHash.Length; i++)
                  {
                      if(PasswordHash[i] != ComputeHash[i]){
                        return false;
                      }
                  }
            }
            return true;
        }
    }
}