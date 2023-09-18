using MahApps.Metro.Controls;
using Npgsql;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System;
using System.IO;
using System.Text.RegularExpressions;
using static TSales.MainWindow;
using ControlzEx.Theming;

namespace TSales.Views
{
    public partial class ExibProd : MetroWindow
    {
        public ExibProd()
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
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal margemLucro;
            decimal qtdeEstoque;
            if (txbLucro.Text == "")
            {
                margemLucro = 0;
            }
            else margemLucro = decimal.Parse(txbLucro.Text);
            if (txbEstoque.Text == "")
            {
                qtdeEstoque = 0;
            }
            else qtdeEstoque = decimal.Parse(txbEstoque.Text);

            var connection = DbConnectionManager.Instance.OpenConnection();
            string sql = "UPDATE public.produtos SET " +
                "codigo = @Codigo, " +
                "descricao = @Descri, " +
                "custounitario = @CustoUnit, " +
                "precovenda = @PrecoVend, " +
                "grupo = @Grupo, " +
                "unidade = @Unidade, " +
                "margemlucro = @MargemLucro," +
                "estoque = @Estoque, " +
                "codean = @Codean " +
                "WHERE codigo = @Codigo;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Codigo", int.Parse(txbCodigo.Text));
            cmd.Parameters.AddWithValue("@Descri", txbDescri.Text);
            cmd.Parameters.AddWithValue("@CustoUnit", decimal.Parse(txbCusto.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@PrecoVend", decimal.Parse(txbPrice.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@Grupo", txbGrupo.Text);
            cmd.Parameters.AddWithValue("@Unidade", txbUnid.Text);
            cmd.Parameters.AddWithValue("@MargemLucro", margemLucro);
            cmd.Parameters.AddWithValue("@Estoque", qtdeEstoque);
            cmd.Parameters.AddWithValue("@Codean", txbCodean.Text);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Dados alterados com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            DbConnectionManager.Instance.CloseConnection();
            this.Close();
        }
        private void txbCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
        private void txbCusto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
        private void txbGrupo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbUnid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbLucro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbCodean_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private Button? FindSaveButton()
        {
            PageCadastro? cadastrosave = Application.Current.MainWindow as PageCadastro;
            return cadastrosave?.FindName("btnSave") as Button;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txbCodigo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
        private void txbCusto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Label_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbEstoque_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbLucro_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbCodean_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txbCodigo.IsEnabled = false;
            txbDescri.IsEnabled = true;
            txbCusto.IsEnabled = true;
            txbPrice.IsEnabled = true;
            txbGrupo.IsEnabled = true;
            txbUnid.IsEnabled = true;
            txbLucro.IsEnabled = true;
            txbEstoque.IsEnabled = true;
            txbCodean.IsEnabled = true;
            btnSave.IsEnabled = true;
        }
    }
}