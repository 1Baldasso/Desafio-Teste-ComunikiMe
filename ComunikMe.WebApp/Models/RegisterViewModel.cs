using ComunikiMe.Domain.DTO;
using System.ComponentModel.DataAnnotations;

namespace ComunikiMe.WebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nome não pode ser vazio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email não pode ser vazio.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Email Inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Confirmação de Email não pode ser vazio.")]
        [Display(Name = "Confirmar email")]
        [Compare(nameof(Email),ErrorMessage = "Deve ser Igual" + nameof(Email))]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Senha não pode ser vazio.")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmação de Senha não pode ser vazio.")]
        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public RegistroDTO ToRegistroDTO()
        {
            return new RegistroDTO()
            {
                Nome = Nome,
                Email = Email,
                Senha = Password
            };
        }

    }
}
