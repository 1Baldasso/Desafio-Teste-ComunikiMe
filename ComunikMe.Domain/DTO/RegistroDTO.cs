using System.ComponentModel.DataAnnotations;

namespace ComunikiMe.Domain.DTO
{
    public class RegistroDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public bool isValidRequest()
        {
            return !(String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Senha));
        }
        public string validateRequest()
        {
            if (!isValidRequest()) return "Todos os campos devem estar preenchidos.";
            if (!(new EmailAddressAttribute().IsValid(this.Email))) return "Email inválido";
            return String.Empty;
        }
    }
}
