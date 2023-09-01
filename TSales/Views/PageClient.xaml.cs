using MahApps.Metro.Controls;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using TSales.Classes;
using static TSales.MainWindow;

namespace TSales.Views {
    public partial class PageClient : MetroWindow {
        List<Clientes> clientes = new List<Clientes>();
        public PageClient() {
            InitializeComponent();
            if (txbSearch.Focus()) {
                clientes.Clear();
                Rel();
            }
        }
        public void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            Rel();
        }
        public void Rel() {
            Retorno.ItemsSource = null;
            var connection = DbConnectionManager.Instance.OpenConnection();
            string Clients = "SELECT codigo, nome, cpfcnpj FROM clientes;";
            NpgsqlCommand cmd = new NpgsqlCommand(Clients, connection);
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
            DbConnectionManager.Instance.CloseConnection();
        }
        private void btnExit_Click(object sender, System.Windows.RoutedEventArgs e) {
            this.Close();
        }
        private void btnNew_Click(object sender, System.Windows.RoutedEventArgs e) {
            PageCadastro pageCadastro = new PageCadastro();
            pageCadastro.ShowDialog();
            clientes.Clear();
            Rel();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            if (Retorno.SelectedItem != null) {
                var selectedItem = (Clientes)Retorno.SelectedItem;
                MessageBoxResult resul = MessageBox.Show($"Tem certeza que deseja excluir o cliente {selectedItem.Nome}?", "Atenção", MessageBoxButton.YesNo);
                if (resul == MessageBoxResult.Yes) {
                    deleteItens();
                }
            }
        }
        public void deleteItens() {
            var connection = DbConnectionManager.Instance.OpenConnection();
            if (Retorno.SelectedItem != null) {
                var selectedItem = (Clientes)Retorno.SelectedItem;
                try {
                    using (NpgsqlConnection con = new NpgsqlConnection(connection.ConnectionString)) {
                        string deleteQuery = $"DELETE FROM clientes WHERE codigo = {selectedItem.Codigo}";
                        using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, connection)) {
                            command.ExecuteNonQuery();
                        }
                    }
                    clientes.Remove(selectedItem);
                    Retorno.Items.Refresh();
                } catch (Exception ex) {
                    MessageBox.Show("Ocorreu um erro ao excluir o item: " + ex.Message);
                }
            }
            Retorno.Items.Refresh();
            DbConnectionManager.Instance.CloseConnection();
        }
        public void selectItens() {
            var connection = DbConnectionManager.Instance.OpenConnection();
            if (Retorno.SelectedItem != null) {
                var selectedItem = (Clientes)Retorno.SelectedItem;
                try {
                    using (NpgsqlConnection con = new NpgsqlConnection(connection.ConnectionString)) {
                        string selectQuery = $"SELECT codigo, nome, cpfcnpj, cidade, telefone, email, cr FROM public.clientes WHERE codigo = {selectedItem.Codigo}";
                        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection)) {
                            NpgsqlDataReader readerClients = command.ExecuteReader();
                            ExibClient newExib = new ExibClient();
                            readerClients.Read();
                            newExib.txbCodigo.Text = readerClients.GetInt32(0).ToString();
                            newExib.txbNome.Text = readerClients.GetString(1);
                            newExib.txbCpfcnpj.Text = readerClients.GetString(2);  
                            newExib.txbCidade.Text = readerClients.GetString(3);
                            newExib.txbTelefone.Text = readerClients.GetString(4);
                            newExib.txbEmail.Text = readerClients.GetString(5);
                            newExib.txbCr.Text = readerClients.GetString(6);
                            connection.Close();
                            newExib.ShowDialog();                                                   
                        }
                    }                    
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            clientes.Clear();
            Rel();
            DbConnectionManager.Instance.CloseConnection();                     
        }
        private void txbSearch_Click(object sender, RoutedEventArgs e) {
            clientes.Clear();
            Rel();
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e) {
            selectItens();
        }
    }
}