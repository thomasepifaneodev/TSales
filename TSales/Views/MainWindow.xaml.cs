using MahApps.Metro.Controls;
using TSales.Views;
using TSales.Classes;
using Npgsql;

namespace TSales {
    public partial class MainWindow : MetroWindow {

        ConnectDb connect = new ConnectDb();        
        public MainWindow() {
            InitializeComponent();
            connect.ConnectionDb();
        }
        private void btnCliente_Click(object sender, System.Windows.RoutedEventArgs e) {
            PageClient page = new PageClient();
            page.ShowDialog();
        }
    }
}