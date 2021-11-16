using System.Threading.Tasks;
using MusicStore.Data;

namespace MusicStore.Business.Interfaces
{
    public interface IUserService
    {
        Task UpdateUsersName(ApplicationUser user);
        Task RemoveUser(string id);
    }
}