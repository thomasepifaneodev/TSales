using Npgsql.Internal.TypeHandlers.NumericHandlers;
using System;
using System.Data.SqlTypes;

namespace TSales.Classes
{    
    internal class Pedido        
    {        
        public int Codigo { get; set; }
        public string? Cliente { get; set; }
        public double QtdeItens { get; set; }
        public decimal PrecoTotal { get; set; }                      
        public string? FormaPagto { get; set; }        
        public string? Usuario { get; set; }
        public DateTime DataVenda { get; set; }
    }
}