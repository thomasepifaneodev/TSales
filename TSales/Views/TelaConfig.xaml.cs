using MahApps.Metro.Controls;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System;
using ControlzEx.Theming;

namespace TSales.Views
{
    public partial class TelaConfig : MetroWindow
    {
        IniFile ini = new IniFile();
        public string Ip;
        public string Base;
        public string Porta;
        public string User;
        public TelaConfig()
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
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            //SALVANDO ALTERAÇÕES DO ARQUIVO INI
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TSales.ini");
            var ip = txbIp.Text;
            var port = txbPorta.Text;
            var db = txbBase.Text;

            var ini = new IniFile();

            ini.Write("ip", $"{ip}");
            ini.Write("port", $"{port}");
            ini.Write("base", $"{db}");

            MessageBox.Show("Dados Salvos com sucesso!", "ZCopy", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LENDO ARQUIVO INI
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TSales.ini");
            if (!File.Exists(path))
            {
                ini.Write("ip", "127.0.0.1");
                ini.Write("port", "5432");
                ini.Write("base", "base_");
            }

            var ip = ini.Read("ip");
            var porta = ini.Read("port");
            var db = ini.Read("base");

            txbIp.Text = ip;
            txbPorta.Text = porta;
            txbBase.Text = db;

            Ip = ip;
            Porta = porta;
            Base = db;
            txbIp.Focus();
        }
        private void txbIp_KeyDown(object sender, KeyEventArgs e)
        {
            //PULANDO DE UMA TEXTBOX PARA OUTRA
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Impede que o espaço seja inserido na caixa de texto
            }
        }
        private void txbPorta_KeyDown(object sender, KeyEventArgs e)
        {
            //PULANDO DE UMA TEXTBOX PARA OUTRA
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
        private void txbBase_KeyDown(object sender, KeyEventArgs e)
        {
            //PULANDO DE UMA TEXTBOX PARA O BOTÃO SALVAR
            if (e.Key == Key.Enter)
            {
                btnSalvar_Click(sender, e);
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Impede que o espaço seja inserido na caixa de texto
            }
        }
        private void txbIp_GotFocus(object sender, RoutedEventArgs e)
        {
            // POSICIONANDO O CURSOR NO FINAL DO TEXTO
            if (sender is TextBox textBox)
            {
                textBox.Focus();
                textBox.Select(txbIp.Text.Length, 0);
            }
        }
        private void txbPorta_GotFocus(object sender, RoutedEventArgs e)
        {
            // POSICIONANDO O CURSOR NO FINAL DO TEXTO
            if (sender is TextBox textBox)
            {
                textBox.Focus();
                textBox.Select(txbPorta.Text.Length, 0);
            }
        }
        private void txbBase_GotFocus(object sender, RoutedEventArgs e)
        {
            // POSICIONANDO O CURSOR NO FINAL DO TEXTO
            if (sender is TextBox textBox)
            {
                textBox.Focus();
                textBox.Select(txbBase.Text.Length, 0);
            }
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //FECHANDO A APLICAÇÃO CASO O BOTÃO ESC SEJA PRESSIONADO
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                btnFechar_Click(sender, e);
            }
        }
        private void txbPorta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica se o texto inserido contém apenas dígitos numéricos
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Impede a inserção de caracteres não numéricos
            }
        }
        private bool IsNumeric(string text)
        {
            // Verifica se a string consiste apenas de dígitos numéricos
            return int.TryParse(text, out _);
        }
        private void txbPorta_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Impede o processamento da tecla espaço
            }
        }
        private void txbIp_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Impede o processamento da tecla espaço
            }
        }
        private void txbBase_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Impede o processamento da tecla espaço
            }
        }
        private void txbIp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbBase_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (
                e.Text.Contains("'") || e.Text.Contains("´") || e.Text.Contains("`") || e.Text.Contains("!") || e.Text.Contains("#") || e.Text.Contains("$") || e.Text.Contains("%") || e.Text.Contains("¨") || e.Text.Contains("&") || e.Text.Contains("*") ||
                e.Text.Contains("(") || e.Text.Contains(")") || e.Text.Contains("?") || e.Text.Contains(":") || e.Text.Contains("<") || e.Text.Contains(">") || e.Text.Contains("/") || e.Text.Contains("|") || e.Text.Contains("\\") || e.Text.Contains("{") ||
                e.Text.Contains("}") || e.Text.Contains("[") || e.Text.Contains("]") || e.Text.Contains(";") || e.Text.Contains("^") || e.Text.Contains("~") || e.Text.Contains("ª") || e.Text.Contains("º") || e.Text.Contains("°") || e.Text.Contains("\"") ||
                e.Text.Contains(",") || e.Text.Contains("ç") || e.Text.Contains("Ç") || e.Text.Contains("=") || e.Text.Contains("-") || e.Text.Contains("+") || e.Text.Contains("§") || e.Text.Contains("@")
                )
            {
                e.Handled = true;
            }
        }
    }
}
