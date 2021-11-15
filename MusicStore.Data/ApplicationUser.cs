using Microsoft.AspNetCore.Identity;

namespace MusicStore.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}