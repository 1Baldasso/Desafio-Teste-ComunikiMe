using ComunikiMe.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComunikiMe.WebApp.Models
{
    public class ProdutoViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Valor não pode ser vazio.")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Estoque não pode ser vazio.")]
        [Range(0, double.MaxValue, ErrorMessage = "O estoque deve ser maior que 0")]
        public int Estoque { get; set; }

        public void ArrangeDecimals()
        {
            this.Valor /= 100;
        }
        public ProdutoViewModel() { }
        public ProdutoViewModel(Produto produto) {
            this.Nome = produto.Nome;
            this.Valor = produto.Valor;
            this.Estoque = produto.Estoque;
        }
    }
}
