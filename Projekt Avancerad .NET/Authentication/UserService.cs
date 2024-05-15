

using Microsoft.EntityFrameworkCore;
using Projekt_Avancerad_.NET.Data;

namespace Projekt_Avancerad_.NET.Authentication
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        public UserService(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<string>> GetRolesAsync(string username)
        {
            var user = await _appDbContext.LoginInfos.FirstOrDefaultAsync(u => u.EMail == username);
            if (user != null) { }
            {
                var roles = new List<string> { user.Role};
                return roles;
            }
            return Enumerable.Empty<string>();
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _appDbContext.LoginInfos.FirstOrDefaultAsync(u => u.EMail == username && u.Password == password);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
