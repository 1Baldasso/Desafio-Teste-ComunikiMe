using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComunikiMe.Domain
{
    [Index("Email", IsUnique = true,Name = "IX_Email")]
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        public bool isAdmin { get; set; }
        public Guid Secret { get; private set; }

        [Column("PasswordHash")]
        public string PasswordHash { get; private set; }

        public bool Register(string password)
        {
            try
            {
                PasswordHasher hasher = new PasswordHasher();
                PasswordHash = hasher.HashPassword(password);
                Secret = Guid.NewGuid();
            } catch
            {
                return false;
            }
            return true;
        }

        public bool Login(string password)
        {
            PasswordHasher hasher = new PasswordHasher();
            var result = hasher.VerifyHashedPassword(PasswordHash,password);
            var senhaValida = result == PasswordVerificationResult.Success;
            if (senhaValida)
                Secret = Guid.NewGuid();
            return senhaValida;
        }
    }
}
