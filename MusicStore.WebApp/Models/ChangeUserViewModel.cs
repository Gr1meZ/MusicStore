using System.ComponentModel.DataAnnotations;

namespace MusicStore.WebApp.Models
{
    public class ChangeUserViewModel
    {
        [Required(ErrorMessage = "The Name field is must be required")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", ErrorMessage = "Surname field is not match with rules")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Name field is must be required")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", ErrorMessage = "Surname field is not match with rules")]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}