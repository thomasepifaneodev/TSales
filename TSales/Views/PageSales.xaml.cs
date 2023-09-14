using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using TSales.Classes;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Globalization;

namespace TSales.Views
{
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
            string Prod = "SELECT codigo, cliente, precototal FROM pedidos;";
            NpgsqlCommand cmd = new NpgsqlCommand(Prod, connection);
            NpgsqlDataReader readerPedido = cmd.ExecuteReader();
            while (readerPedido.Read())
            {
                Pedido pedido = new Pedido
                {
                    Codigo = readerPedido.GetInt32(0),
                    Cliente = readerPedido.GetString(1),
                    PrecoTotal = readerPedido.GetDecimal(2),
                };                
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
