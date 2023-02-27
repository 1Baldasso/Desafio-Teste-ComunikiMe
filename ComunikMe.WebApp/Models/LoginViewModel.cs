using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace ComunikiMe.WebApp.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
