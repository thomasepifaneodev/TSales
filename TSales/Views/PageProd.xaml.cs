using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using TSales.Classes;
using System.Collections.Generic;
using System.Globalization;

namespace TSales.Views {
    public partial class PageProd : MetroWindow {
        List<Produtos> produtos = new List<Produtos>();
        public PageProd() {
            InitializeComponent();
        }
        public void Rel() {
            Retorno.ItemsSource = null;
            var connection = DbConnectionManager.Instance.OpenConnection();
            string Clients = "SELECT codigo, descricao, ROUND(precovenda, 2) FROM produtos;";
            NpgsqlCommand cmd = new NpgsqlCommand(Clients, connection);
            NpgsqlDataReader readerClients = cmd.ExecuteReader();
            while (readerClients.Read()) {
                Produtos produto = new Produtos {
                    Codigo = readerClients.GetInt32(0),
                    Descricao = readerClients.GetString(1),
                    PrecoVenda = readerClients.GetDouble(2),                                                      
            };
                produto.PrecoVendaFormatado = string.Format("R$ {0:0.00}", produto.PrecoVenda);
                produtos.Add(produto);
            }
            readerClients.Close();
            Retorno.ItemsSource = produtos;
            Retorno.Items.Refresh();
            DbConnectionManager.Instance.CloseConnection();
        }
        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            Rel();
        }
        private void btnNew_Click(object sender, System.Windows.RoutedEventArgs e) {
            PageCadastroProd prod = new PageCadastroProd();
            prod.ShowDialog();
        }
    }
}
