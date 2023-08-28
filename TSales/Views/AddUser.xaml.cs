using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using System.Windows;
using System;

namespace TSales.Views {
    public partial class AddUser : MetroWindow {
        bool rows;
        public AddUser() {
            InitializeComponent();
            txbUser.Focus();
        }
        private void btnSalvar_Click(object sender, System.Windows.RoutedEventArgs e) {                        
            var connection = DbConnectionManager.Instance.OpenConnection();
            bool check = false;
            if (checkAdm.IsChecked == true) { 
            check = true;
            }else check = false;
                try {
                string sql = "SELECT insertuser(@Login, @Senha, @Admin)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Login", txbUser.Text);
                cmd.Parameters.AddWithValue("@Senha", txbPass.Password);
                cmd.Parameters.AddWithValue("@Admin", check);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                rows = reader.HasRows;
                //string sql2 = $"CREATE USER {txbUser.Text} WITH ENCRYPTED PASSWORD '{txbPass.Password}';";
                //NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, connection);
                //NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                if (rows == true) {
                    MessageBox.Show("Dados inseridos com sucesso!");
                }
            } catch (Exception) {
                MessageBox.Show("Preencha todos os dados!");
            }
            DbConnectionManager.Instance.CloseConnection();
            txbUser.Clear();
            txbPass.Clear();
            checkAdm.IsChecked = false;
        }
        private void btnSair_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
