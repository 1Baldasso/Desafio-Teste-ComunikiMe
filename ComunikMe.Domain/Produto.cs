using System.ComponentModel.DataAnnotations;

namespace ComunikiMe.Domain
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        public decimal Valor { get; set; } = 0;
        public int Estoque { get; set; } = 0;
    }
}