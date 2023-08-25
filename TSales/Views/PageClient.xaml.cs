using MahApps.Metro.Controls;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using TSales.Classes;

namespace TSales.Views {
    public partial class PageClient : MetroWindow {

        ConnectDb connect = new ConnectDb();
        List<Clientes> clientes = new List<Clientes>();
        NpgsqlConnection conn = new NpgsqlConnection();
        public PageClient() {
            InitializeComponent();
            connect.ConnectionDb();
            conn.ConnectionString = connect.ConnectionString();
        }
        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            conn.Open();
            string Clients = "SELECT codigo, nome, cpfcnpj FROM clientes;";
            NpgsqlCommand cmd = new NpgsqlCommand(Clients, conn);
            NpgsqlDataReader readerClients = cmd.ExecuteReader();
            while (readerClients.Read()) {
                Clientes cliente = new Clientes {
                    Codigo = readerClients.GetInt32(0),
                    Nome = readerClients.GetString(1),
                    Cpfcnpj = readerClients.GetString(2),
                };
                clientes.Add(cliente);
            }
            readerClients.Close();
            Retorno.ItemsSource = clientes;
            Retorno.Items.Refresh();
        }
        private void btnExit_Click(object sender, System.Windows.RoutedEventArgs e) {
            this.Close();
        }
    }
}
