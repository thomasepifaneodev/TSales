using MahApps.Metro.Controls;
using Npgsql;
using static TSales.MainWindow;
using TSales.Classes;
using System.Collections.Generic;
using System.Windows;
using System;

namespace TSales.Views
{
    public partial class PageProd : MetroWindow
    {
        List<Produtos> produtos = new List<Produtos>();
        public PageProd()
        {
            InitializeComponent();
        }
        public void Rel()
        {
            Retorno.ItemsSource = null;
            var connection = DbConnectionManager.Instance.OpenConnection();
            string Prod = "SELECT codigo, descricao, ROUND(precovenda, 2) FROM produtos;";
            NpgsqlCommand cmd = new NpgsqlCommand(Prod, connection);
            NpgsqlDataReader readerClients = cmd.ExecuteReader();
            while (readerClients.Read())
            {
                Produtos produto = new Produtos
                {
                    Codigo = readerClients.GetInt32(0),
                    Descricao = readerClients.GetString(1),
                    PrecoVenda = readerClients.GetDouble(2),
                };
                produto.PrecoVendaFormatado = string.Format("R$ {0:0.00}", produto.PrecoVenda);
                produtos.Add(produto);
            }
            readerClients.Close();
            Retorno.ItemsSource = produtos;
            Retorno.Items.Refresh();
            DbConnectionManager.Instance.CloseConnection();
        }
        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Rel();
        }
        private void btnNew_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PageCadastroProd prod = new PageCadastroProd();
            prod.ShowDialog();
            produtos.Clear();
            Rel();
        }
        public void selectItens()
        {
            var connection = DbConnectionManager.Instance.OpenConnection();
            if (Retorno.SelectedItem != null)
            {
                var selectedItem = (Produtos)Retorno.SelectedItem;
                try
                {
                    using (NpgsqlConnection con = new NpgsqlConnection(connection.ConnectionString))
                    {
                        string selectQuery = $"SELECT codigo, descricao, custounitario, precovenda, grupo, unidade, margemlucro, estoque, codean FROM public.produtos WHERE codigo = {selectedItem.Codigo}";
                        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                        {
                            NpgsqlDataReader readerProd = command.ExecuteReader();
                            ExibProd newExib = new ExibProd();
                            readerProd.Read();
                            newExib.txbCodigo.Text = readerProd.GetInt32(0).ToString();
                            newExib.txbDescri.Text = readerProd.GetString(1);                            
                            newExib.txbCusto.Text = readerProd.GetDouble(2).ToString();
                            newExib.txbPrice.Text = readerProd.GetDouble(3).ToString();
                            newExib.txbGrupo.Text = readerProd.GetString(4);
                            newExib.txbUnid.Text = readerProd.GetString(5);                            
                            newExib.txbLucro.Text = readerProd.GetDouble(6).ToString();
                            newExib.txbEstoque.Text = readerProd.GetDouble(7).ToString();
                            newExib.txbCodean.Text = readerProd.GetString(8);
                            connection.Close();
                            newExib.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            produtos.Clear();
            Rel();
            DbConnectionManager.Instance.CloseConnection();
        }
        private void btnBuscar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectItens();
        }
        private void btnExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
