using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using TSales.Classes;
using System.Windows.Documents;
using System.Collections.Generic;

namespace TSales.Views
{
    /// <summary>
    /// Lógica interna para PageSales.xaml
    /// </summary>
    public partial class PageSales : MetroWindow
    {
        List<Pedido> pedidos = new List<Pedido>();  
        public PageSales()
        {
            InitializeComponent();
        }
        public void Rel()
        {
            Retorno.ItemsSource = null;
            var connection = DbConnectionManager.Instance.OpenConnection();
            string Prod = "SELECT codigo, cliente, ROUND(precototal, 2) FROM pedidos;";
            NpgsqlCommand cmd = new NpgsqlCommand(Prod, connection);
            NpgsqlDataReader readerPedido = cmd.ExecuteReader();
            while (readerPedido.Read())
            {
                Pedido pedido = new Pedido
                {
                    Codigo = readerPedido.GetInt32(0),
                    Cliente = readerPedido.GetString(1),
                    PrecoTotal = readerPedido.GetDouble(2),
                };
                pedido.PrecoTotalFormatado = string.Format("R$ {0:0.00}", pedido.PrecoTotal);
                pedidos.Add(pedido);
            }
            readerPedido.Close();
            Retorno.ItemsSource = pedidos;
            Retorno.Items.Refresh();
            DbConnectionManager.Instance.CloseConnection();
        }
        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Rel();
        }       
    }
}
