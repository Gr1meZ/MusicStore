using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MusicStore.Data;

namespace MusicStore.WebApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "The Name field is must be required")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Name field is not match with rules")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
            
        [Required(ErrorMessage = "The Name field is must be required")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$", ErrorMessage = "Surname field is not match with rules")]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }


    }
}