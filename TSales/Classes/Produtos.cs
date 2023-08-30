namespace TSales.Classes {
    class Produtos {
        public int Codigo { get; set; }
        public string? Descricao { get; set; }
        public double CustoUnitario { get; set; }
        public double PrecoVenda { get; set; }
        public string PrecoVendaFormatado { get; set; }
        public string? Grupo { get; set; }
        public string? Unidade { get; set; }
        public double MargemLucro { get; set; }

    }
}
