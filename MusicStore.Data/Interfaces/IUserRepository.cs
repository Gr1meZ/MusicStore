using System.Threading.Tasks;

namespace MusicStore.Data.Interfaces
{
    public interface IUserRepository
    {
        Task UpdateName(ApplicationUser user);
        Task<ApplicationUser> Get(string id);
        Task Remove(string id);
    }
}