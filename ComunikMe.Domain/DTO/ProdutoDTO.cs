using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.DTO
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Estoque { get; set; }

        public Produto ToProduto()
        {
            return new Produto()
            {
                Nome = Nome,
                Valor = Valor,
                Estoque = Estoque
            };
        }
    }
}
