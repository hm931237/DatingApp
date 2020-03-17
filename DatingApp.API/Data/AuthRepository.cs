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
        public Task<User> Login (string username, string Password)
        {
            throw new system.NotImplementedException();
        }
        public Task<bool> UserExists (string username)
        {
            throw new system.NotImplementedException();
        }
         
        private void CreatePasswordHash(string Password,out byte[] PasswordHash,out byte[] PasswordSalt)
        {
            using (var hmac =new System.Security.Cryptography.HMACSHA512())
            {
                  PasswordSalt = hmac.Key; 
                  PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

    }
}