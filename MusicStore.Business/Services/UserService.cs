using System.Threading.Tasks;
using MusicStore.Business.Interfaces;
using MusicStore.Data;
using MusicStore.Data.Interfaces;

namespace MusicStore.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UpdateUsersName(ApplicationUser user)
        {
            await _userRepository.UpdateName(user);
        }

        public async Task RemoveUser(string id)
        {
            await _userRepository.Remove(id);
        }
    }
}