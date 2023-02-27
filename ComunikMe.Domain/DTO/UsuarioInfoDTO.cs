using ComunikiMe.Domain;

namespace ComunikiMe.Domain.DTO
{
    public class UsuarioInfoDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
        public Guid Secret { get; set; }
        public UsuarioInfoDTO() { }
        public UsuarioInfoDTO(Usuario usuario) {
            this.Nome = usuario.Nome;
            this.Email = usuario.Email;
            this.Secret = usuario.Secret;
            this.isAdmin = usuario.isAdmin;
        }
    }
}
