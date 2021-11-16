using System.Threading.Tasks;
using MusicStore.Data.Data;
using MusicStore.Data.Interfaces;

namespace MusicStore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateName(ApplicationUser userDto)
        {
            _context.Users.Update(userDto);
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> Get(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Remove(string id)
        {
            ApplicationUser user = await Get(id);
            
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}