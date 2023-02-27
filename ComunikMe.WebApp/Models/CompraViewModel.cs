namespace ComunikiMe.WebApp.Models
{
    public class CompraViewModel
    {
        public decimal Custo { get;private set; }
        public decimal ValorPago { get; set; }
        public int Produto { get; set; }

        public CompraViewModel() { }
        public CompraViewModel(decimal custo) => this.Custo = custo;
    }
}
