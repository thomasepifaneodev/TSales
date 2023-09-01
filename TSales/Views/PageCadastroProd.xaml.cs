using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System;

namespace TSales.Views {
    public partial class PageCadastroProd : MetroWindow {        
        public PageCadastroProd() {
            InitializeComponent();
            txbGrupo.Focus();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e) {
            decimal margemLucro;
            decimal qtdeEstoque;
            if (txbLucro.Text == "") {
                margemLucro = 0;
            } else margemLucro = decimal.Parse(txbLucro.Text);
            if (txbEstoque.Text == "") {
                qtdeEstoque = 0;
            } else qtdeEstoque = decimal.Parse(txbEstoque.Text);

            var connection = DbConnectionManager.Instance.OpenConnection();
            string sql = "INSERT INTO public.produtos VALUES (@Codigo, @Descri, @CustoUnit, @PrecoVend, @Grupo, @Unidade, @MargemLucro, @Estoque, @Codean);";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Codigo", int.Parse(txbCodigo.Text));
            cmd.Parameters.AddWithValue("@Descri", txbDescri.Text);
            cmd.Parameters.AddWithValue("@CustoUnit", decimal.Parse(txbCusto.Text));
            cmd.Parameters.AddWithValue("@PrecoVend", decimal.Parse(txbPrice.Text));
            cmd.Parameters.AddWithValue("@Grupo", txbGrupo.Text);
            cmd.Parameters.AddWithValue("@Unidade", txbUnid.Text);
            cmd.Parameters.AddWithValue("@MargemLucro", margemLucro);
            cmd.Parameters.AddWithValue("@Estoque", qtdeEstoque);
            cmd.Parameters.AddWithValue("@Codean", txbCodean.Text);
            try {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Dados inseridos com sucesso!");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
            DbConnectionManager.Instance.CloseConnection();
            this.Close();
        }
        private void txbCodigo_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbDescricao_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
        private void txbCusto_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
        private void txbGrupo_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbUnid_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbPrice_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbEstoque_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbLucro_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbCodean_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement? elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private Button? FindSaveButton() {
            PageCadastro? cadastrosave = Application.Current.MainWindow as PageCadastro;
            return cadastrosave?.FindName("btnSave") as Button;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
        private void txbCodigo_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text.Contains(".")) {
                e.Handled = true;
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e) {
            var connection = DbConnectionManager.Instance.OpenConnection();
            string sql = $"SELECT nextval('produtos_codigo');";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) {
                // Obtendo o resultado
                object? result = cmd.ExecuteScalar();
                // Verificando se o resultado não é nulo
                if (result != DBNull.Value && result != null) {
                    // Convertendo o resultado para inteiro
                    int maxCodigo = Convert.ToInt32(result);
                    // Exibindo o valor máximo na TextBox
                    txbCodigo.Text = maxCodigo.ToString();
                }
            }
            DbConnectionManager.Instance.CloseConnection();
        }
        private void txbCusto_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Label_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbEstoque_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbLucro_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbCodean_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}