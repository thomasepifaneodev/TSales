﻿using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System;

namespace TSales.Views {
    /// <summary>
    /// Lógica interna para PageCadastroProd.xaml
    /// </summary>
    public partial class PageCadastroProd : MetroWindow {
        bool rows;
        public PageCadastroProd() {
            InitializeComponent();
            txbDescri.Focus();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e) {
            var connection = DbConnectionManager.Instance.OpenConnection();
            try {
                string sql = "SELECT insertprod(@Codigo, @Descri, @CustoUnit, @PrecoVend, @Grupo, @Unidade, @Margem);";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Codigo", int.Parse(txbCodigo.Text));
                cmd.Parameters.AddWithValue("@Descri", txbDescri.Text);
                cmd.Parameters.AddWithValue("@CustoUnit", decimal.Parse(txbCusto.Text));
                cmd.Parameters.AddWithValue("@PrecoVend", decimal.Parse(txbPrice.Text));
                cmd.Parameters.AddWithValue("@Grupo", txbGrupo.Text);
                cmd.Parameters.AddWithValue("@Unidade", txbUnid.Text);
                cmd.Parameters.AddWithValue("@Margem", decimal.Parse(txbLucro.Text));
                NpgsqlDataReader reader = cmd.ExecuteReader();
                rows = reader.HasRows;
                if (rows == true) {
                    MessageBox.Show("Dados inseridos com sucesso!");

                }
            } catch (Exception) {
                MessageBox.Show("Preencha todos os dados!");
            }
            DbConnectionManager.Instance.CloseConnection();
            this.Close();
        }
        private void txbCodigo_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbNome_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
        private void txbCpfcnpj_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
        private void txbTelefone_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbCidade_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbSaldo_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbContas_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbPrazo_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Define o foco para o próximo controle
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null) {
                    elementWithFocus.MoveFocus(request);
                }
                // Impede que o caractere Enter seja digitado no TextBox
                e.Handled = true;
            }
        }
        private void txbEmail_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // Encontre o botão de salvar no visual tree
                Button btnsave = FindSaveButton();

                // Verifique se o botão foi encontrado e execute a ação de clique
                if (btnsave != null) {
                    btnsave.Focus();
                    btnsave.Command.Execute(null);
                }
            }
        }
        private Button FindSaveButton() {
            PageCadastro cadastrosave = Application.Current.MainWindow as PageCadastro;
            return cadastrosave?.FindName("btnSave") as Button;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
        private void txbCpfcnpj_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            // Verifica se o caractere digitado é um número
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) {
                e.Handled = true; // Impede a entrada do caractere
            }
        }
        private void txbCpfcnpj_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox txbCpfcnpj = (TextBox)sender;
            string text = txbCpfcnpj.Text;
            // Remove todos os caracteres não numéricos do texto
            text = new string(text.Where(char.IsDigit).ToArray());
            // Verifica se o texto representa um CPF ou CNPJ
            if (text.Length == 11) {
                // Formata o texto como CPF (ex: 123.456.789-00)
                text = $"{text.Substring(0, 3)}.{text.Substring(3, 3)}.{text.Substring(6, 3)}-{text.Substring(9)}";
            } else if (text.Length == 14) {
                // Formata o texto como CNPJ (ex: 12.345.678/0001-00)
                text = $"{text.Substring(0, 2)}.{text.Substring(2, 3)}.{text.Substring(5, 3)}/{text.Substring(8, 4)}-{text.Substring(12)}";
            }
            txbCpfcnpj.Text = text;
            txbCpfcnpj.CaretIndex = text.Length; // Posiciona o cursor no final do texto formatado
        }

        private void txbTelefone_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            // Verifica se o caractere digitado é um número
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) {
                e.Handled = true; // Impede a entrada do caractere
            }
        }
        private void txbTelefone_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox txbCpfcnpj = (TextBox)sender;
            string text = txbCpfcnpj.Text;
            // Remove todos os caracteres não numéricos do texto
            text = new string(text.Where(char.IsDigit).ToArray());
            // Verifica se o texto representa um TELEFONE
            if (text.Length == 11) {
                // Formata o texto como TELFONE 11 DIGITOS (ex: (79)99647-4782)
                text = $"({text.Substring(0, 2)}){text.Substring(2, 5)}-{text.Substring(7, 4)}";
            } else if (text.Length == 10) {
                // Formata o texto como TELFONE 10 DIGITOS (ex: (79)9647-4782)
                text = $"({text.Substring(0, 2)}){text.Substring(2, 4)}-{text.Substring(6, 4)}";
            }
            txbCpfcnpj.Text = text;
            txbCpfcnpj.CaretIndex = text.Length; // Posiciona o cursor no final do texto formatado
        }
        private void txbCodigo_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text.Contains(".")) {
                e.Handled = true;
            }
        }
        private void txbCr_PreviewTextInput(object sender, TextCompositionEventArgs e) {
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
                object result = cmd.ExecuteScalar();
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
    }
}