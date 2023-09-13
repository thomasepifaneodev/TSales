using System;

namespace TSales.Classes
{
    internal class Pedido
    {
        public int Codigo { get; set; }
        public string? Cliente { get; set; }
        public double QtdeItens { get; set; }
        public double PrecoTotal { get; set; }
        public string PrecoTotalFormatado { get; set; }
        public string? FormaPagto { get; set; }
        public string? Usuario { get; set; }
        public DateTime DataVenda { get; set; }
    }
}