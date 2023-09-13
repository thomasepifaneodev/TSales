using MahApps.Metro.Controls;
using TSales.Views;
using Npgsql;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.IO;
using System;
using System.Data;

namespace TSales
{
    public partial class MainWindow : MetroWindow
    {
        string? userconnect;
        static string? conection;

        IniFile ini = new IniFile();
        NpgsqlConnection connect = new NpgsqlConnection();
        public MainWindow()
        {
            InitializeComponent();
            ini.Write("currentuser", "");
        }
        private async void ShowLogin(object sender, RoutedEventArgs e)
        {
            LoginDialogSettings loginDialogSettings = new LoginDialogSettings
            {
                AffirmativeButtonText = "Login",
                NegativeButtonText = "Cancelar",
                NegativeButtonVisibility = Visibility.Visible,
                UsernameWatermark = "Usuário",
                PasswordWatermark = "Senha",
                UsernameCharacterCasing = System.Windows.Controls.CharacterCasing.Lower,
                AnimateHide = true,
            };
            var loginResult = await this.ShowLoginAsync("TSales 1.0.0.0 - Login", "Informe o usuário e senha:", loginDialogSettings);

            if (loginResult == null)
            {// O usuário clicou em Cancelar
                MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você escolheu sair?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    this.Close();
                }
                else if (result == MessageDialogResult.Negative)
                {
                    MetroWindow_Loaded(sender, e);
                }
                return;
            }
            string username = loginResult.Username;
            string password = loginResult.Password;

            userconnect = username;
            ini.Write("currentuser", username);

            if (IsValidUser(username, password) == "loginaceito")
            {
                IsAdm();
                lblUserLog.Text = "Usuário: " + ini.Read("currentuser");
                lblVersao.Text = "Versão: " + "1.0";
                lblIP.Text = "IP: " + ini.Read("ip");
                lblPort.Text = "Porta: " + ini.Read("port");
                lblBase.Text = "Base: " + ini.Read("base");
                lblVersao.Visibility = Visibility.Visible;
                lblIP.Visibility = Visibility.Visible;
                lblPort.Visibility = Visibility.Visible;
                lblBase.Visibility = Visibility.Visible;
                lblUserLog.Visibility = Visibility.Visible;
            }
            else
            {
                await this.ShowMessageAsync("Login Falhou", "Usuário e/ou senha inválido.");
                MetroWindow_Loaded(sender, e);

            }
        }
        public class DbConnectionManager
        {
            private static readonly Lazy<DbConnectionManager> instance = new Lazy<DbConnectionManager>(() => new DbConnectionManager());
            public static DbConnectionManager Instance => instance.Value;

            private NpgsqlConnection connection;
            private DbConnectionManager()
            {
                connection = new NpgsqlConnection(conection);
            }
            public NpgsqlConnection OpenConnection()
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                return connection;
            }
            public void CloseConnection()
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        private string IsValidUser(string username, string password)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TSales.ini");
                if (!File.Exists(path))
                {
                    ini.Write("ip", "127.0.0.1");
                    ini.Write("port", "5432");
                    ini.Write("base", "base_");
                    ini.Write("currentuser", "");
                }
                var ip = ini.Read("ip");
                var port = ini.Read("port");
                var db = ini.Read("base");

                string con = ($"Server={ip}; Port={port}; Database={db}; User Id={username}; Password={password};");
                connect.Close();
                connect.ConnectionString = con;
                conection = con;
                string sql = $"SELECT pass = md5('{password}') AS verificado FROM users WHERE login = '{username}';";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connect);
                connect.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetBoolean(0))
                    {
                        reader.Close();
                        return "loginaceito";
                    }
                }
                reader.Close();
                return "naopermitido";
            }
            catch (NpgsqlException)
            {
                return "falhanaconexao";
            }
        }
        private void btnCliente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PageClient page = new PageClient();
            page.ShowDialog();
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLogin(sender, e);
        }
        public void IsAdm()
        {
            string sql = $"SELECT adm = TRUE AS verificado FROM users WHERE login = '{userconnect}';";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connect);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetBoolean(0))
                {
                    btnUser.IsEnabled = true;

                }
                else
                {
                    btnUser.IsEnabled = false;
                }
            }
        }
        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser add = new AddUser();
            add.ShowDialog();
        }
        private void btnLogOff_Click(object sender, RoutedEventArgs e)
        {
            MetroWindow_Loaded(sender, e);
        }
        private void btnProd_Click(object sender, RoutedEventArgs e)
        {
            PageProd pageProd = new PageProd();
            pageProd.ShowDialog();
        }
        private void btnVendas_Click(object sender, RoutedEventArgs e)
        {
            PageSales pageSales = new PageSales();
            pageSales.ShowDialog();
        }
    }
}