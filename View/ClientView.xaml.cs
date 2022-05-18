using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Sanator.ModelDb;
using Sanator.ViewModel;

namespace Sanator.View
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        public ClientView(ClientViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            clients = getClients();
        }

        public ObservableCollection<Client> clients { get; set; }

        public ObservableCollection<Client> getClients()
        {
            ObservableCollection<Client> result = new ObservableCollection<Client>();
            string sql = "select * from Client";
            var mySqlDB= MySqlDB.GetDB();

            if(mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(sql, mySqlDB.sqlConnection))
                    using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(new Client
                        {
                            ID_client=dr.GetInt32(""),
                            FIO = dr.GetString(""),
                             passport = dr.GetString(""),
                            NumberPhone= dr.GetInt32("")
                        });
                    }
                }
            }
            return result;
        }
    }
}
