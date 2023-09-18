using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using TSales.Classes;
using System.Collections.Generic;
using ControlzEx.Theming;
using System;
using System.IO;

namespace TSales.Views
{
    public partial class PageSales : MetroWindow
    {
        List<Pedido> pedidos = new List<Pedido>();
        public PageSales()
        {
            InitializeComponent();
            ModoEscuro();
        }
        public void ModoEscuro()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TSales.ini");
            IniFile ini = new IniFile();
            if (!File.Exists(path))
            {
                ini.Write("modo", "claro");
            }
            if (ini.Read("modo") == "claro")
            {
                ThemeManager.Current.ChangeTheme(this, "Light.Cobalt");
            }
            else
            {
                ThemeManager.Current.ChangeTheme(this, "Dark.Cobalt");
            }
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
