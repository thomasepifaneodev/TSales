using MahApps.Metro.Controls;
using TSales.Views;
using TSales.Classes;
using Npgsql;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.IO;
using System;

namespace TSales {
    public partial class MainWindow : MetroWindow {
        
        IniFile ini = new IniFile();
        public NpgsqlConnection connect = new NpgsqlConnection();
        public MainWindow() {
            InitializeComponent();
        }
        private async void ShowLogin(object sender, RoutedEventArgs e) {
            var loginDialogSettings = new LoginDialogSettings {
                AffirmativeButtonText = "Login",
                NegativeButtonText = "Cancel",
                AnimateHide = true
            };

            var loginResult = await this.ShowLoginAsync("Login", "Enter your credentials:", loginDialogSettings);

            if (loginResult == null) // O usuário clicou em Cancelar
                return;

            string username = loginResult.Username;
            string password = loginResult.Password;            

            // Agora você pode verificar as credenciais inseridas pelo usuário e tomar ação apropriada.
            if (IsValidUser(username, password) == "loginaceito") {
                // Usuário válido, faça o que for necessário após o login bem-sucedido.
            } else {
                await this.ShowMessageAsync("Login Failed", "Invalid username or password.");
                MetroWindow_Loaded(sender, e);
            }
        }
        private string IsValidUser(string username, string password) {
            try {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TSales.ini");
                if (!File.Exists(path)) {
                    ini.Write("ip", "127.0.0.1");
                    ini.Write("port", "5432");
                    ini.Write("base", "base_");
                }
                var ip = ini.Read("ip");
                var port = ini.Read("port");
                var db = ini.Read("base");
                string con = ($"Server={ip}; Port={port}; Database={db}; User Id={username}; Password={password};");
                connect.Close();
                connect.ConnectionString = con;
                string sql = $"SELECT pass = md5('{password}') AS verificado FROM users WHERE login = '{username}';";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connect);
                connect.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    if (reader.GetBoolean(0)) {
                        reader.Close();
                        return "loginaceito";
                    }
                }
                reader.Close();
                return "naopermitido";
            } catch (NpgsqlException) {
                return "falhanaconexao";
            }
        }
        private void btnCliente_Click(object sender, System.Windows.RoutedEventArgs e) {
            PageClient page = new PageClient();
            page.ShowDialog();
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e) {
            ShowLogin(sender, e);
        }
    }
}