using System;
using System.IO;
using Npgsql;

namespace TSales.Classes {
    class ConnectDb {

        string Connect;
        public string ConnectionString (){
            return Connect;
        }    
        
        public void ConnectionDb() {
            IniFile ini = new IniFile();
            NpgsqlConnection conn = new NpgsqlConnection();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TSales.ini");

            if (!File.Exists(path)) {
                ini.Write("ip", "127.0.0.1");
                ini.Write("port", "5432");
                ini.Write("base", "base_tsales");
            }

            var ip = ini.Read("ip");
            var porta = ini.Read("porta");
            var db = ini.Read("base");

            //try {
            string con = ($"Server={ip}; Port={porta}; Database=base_tsales; User Id=thomas; Password=@2t24F5D4n75Z8foE8541Gj54gS5+878a@341R5$sGa4ES5$j%D14s#5d!5;");
            conn.ConnectionString = con;
            Connect = conn.ConnectionString;    
            //} catch (Exception ex) {
            //    MessageBox.Show("Configure as informa��es de conex�o!");
            //    TelaConfig telaConfig = new TelaConfig();
            //    telaConfig.ShowDialog();
            //    ip = ini.Read("ip");
            //    porta = ini.Read("porta");
            //    db = ini.Read("base");
            //}
        }
    }
}
