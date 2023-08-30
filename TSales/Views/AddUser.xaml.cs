using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using System.Windows;
using System;

namespace TSales.Views {
    public partial class AddUser : MetroWindow {
        bool check = false;
        bool rows;
        public AddUser() {
            InitializeComponent();
            txbUser.Focus();
        }
        private void btnSalvar_Click(object sender, System.Windows.RoutedEventArgs e) {            
            try {
                CreateUser();
                CreatePgUser();
                PermiUser();                
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            txbUser.Clear();
            txbPass.Clear();
            checkAdm.IsChecked = false;
            txbUser.Focus();
        }
        public void CreateUser() {
            var connection = DbConnectionManager.Instance.OpenConnection();
            if (checkAdm.IsChecked == true) {
                check = true;
            } else check = false;
            try {
                string sql = "SELECT insertuser((SELECT MAX(codigo) + 1 codigomaximo FROM users), @Login, @Senha, @Admin);";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Login", txbUser.Text);
                cmd.Parameters.AddWithValue("@Senha", txbPass.Password);
                cmd.Parameters.AddWithValue("@Admin", check);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                rows = reader.HasRows;
                if (rows == true) {
                    MessageBox.Show("Dados inseridos com sucesso!");
                }
            } catch (Exception) {
                MessageBox.Show("Preencha todos os dados!");
            }
            DbConnectionManager.Instance.CloseConnection();
        }
        public void CreatePgUser() {
            var connection = DbConnectionManager.Instance.OpenConnection();
            try {
                if (check == false) {
                    string sql2 = "SELECT insertuserpg(@Login, @Senha);";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, connection);
                    cmd2.Parameters.AddWithValue("@Login", txbUser.Text);
                    cmd2.Parameters.AddWithValue("@Senha", txbPass.Password);
                    NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                    DbConnectionManager.Instance.CloseConnection();
                } else {
                    string sql2 = "SELECT insertsuperuser(@Login, @Senha);";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, connection);
                    cmd2.Parameters.AddWithValue("@Login", txbUser.Text);
                    cmd2.Parameters.AddWithValue("@Senha", txbPass.Password);
                    NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                    DbConnectionManager.Instance.CloseConnection();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            DbConnectionManager.Instance.CloseConnection();
        }
        public void PermiUser() {
            var connection = DbConnectionManager.Instance.OpenConnection();
            try {
                if (check == false) {
                    string sql3 = "SELECT pg_grant(@Login,'SELECT, INSERT, UPDATE','public');";
                    NpgsqlCommand cmd3 = new NpgsqlCommand(sql3, connection);
                    cmd3.Parameters.AddWithValue("@Login", txbUser.Text);
                    NpgsqlDataReader reader3 = cmd3.ExecuteReader();
                } else if (check == true) {
                    string sql4 = $"SELECT pg_grant(@Login, 'ALL', 'public');";
                    NpgsqlCommand cmd4 = new NpgsqlCommand(sql4, connection);
                    cmd4.Parameters.AddWithValue("@Login", txbUser.Text);
                    NpgsqlDataReader reader4 = cmd4.ExecuteReader();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            DbConnectionManager.Instance.CloseConnection();
        }
        private void btnSair_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
