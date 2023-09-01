using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using System.Windows;
using System;

namespace TSales.Views {
    public partial class AddUser : MetroWindow {
        bool check = false;
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
                MessageBox.Show(ex.Message.ToString());
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

            string sql = "INSERT INTO public.users VALUES((SELECT nextval('users_codigo')), @Login, MD5(@Senha), @Admin);";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Login", txbUser.Text);
            cmd.Parameters.AddWithValue("@Senha", txbPass.Password);
            cmd.Parameters.AddWithValue("@Admin", check);
            try {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Dados inseridos com sucesso!");
            } catch (Exception ex) {
                if (ex.ToString().Contains("duplicate key value violates unique constraint")) {
                    MessageBox.Show("Usuário já existe na base de dados!");
                } else {
                    MessageBox.Show(ex.ToString());
                }
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
                    string sql2 = "SELECT insertuserpgsuper(@Login, @Senha);";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, connection);
                    cmd2.Parameters.AddWithValue("@Login", txbUser.Text);
                    cmd2.Parameters.AddWithValue("@Senha", txbPass.Password);
                    NpgsqlDataReader reader2 = cmd2.ExecuteReader();
                    DbConnectionManager.Instance.CloseConnection();
                }
            } catch (Exception ex) {
                if (ex.ToString().Contains("already exists")) {
                    //MessageBox.Show("Usuário já existe na base de dados");
                } else {
                    MessageBox.Show(ex.Message.ToString());
                }
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