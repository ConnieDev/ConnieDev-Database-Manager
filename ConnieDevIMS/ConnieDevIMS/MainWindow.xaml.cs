using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ConnieDevIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataProvider DPServer = new DataProvider();
        DataProvider DPLocal;

        public MainWindow()
        {
            DPLocal = new DataProvider();
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (TText.Text != "" && SDText.Text != "" && LDText.Text != "" && IText.Text != "") {
                String[] data = new String[4];
                data[0] = TText.Text;
                data[1] = SDText.Text;
                data[2] = LDText.Text;
                data[3] = IText.Text;
                DPServer.InsertData(data);
                UpdateTable();

                TText.Text = "";
                SDText.Text = "";
                LDText.Text = "";
                IText.Text = "";
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (DSText.Text != "" && ICText.Text != "" && UIText.Text != "" && PText.Password != "")
            {
                String DS = DSText.Text;
                String IC = ICText.Text;
                String UI = UIText.Text;
                String P = PText.Password;
                String connectionPath = @"Data Source=" + DS + ";Initial Catalog=" + IC + ";User Id=" + UI + ";Password=" + P + ";";
                DPServer.ConnectTo(connectionPath);
                Connector.IsEnabled = false;
                SaveCon.IsEnabled = true;
                ItemInput.IsEnabled = true;
                DataEditor.IsEnabled = true;
                UpdateTable();
            }

        }

        private void SaveCon_Click(object sender, RoutedEventArgs e)
        {
            DPLocal.ConnectTo(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Connections.mdf;Integrated Security=True");
            String[] data = new String[3];
            data[0] = DSText.Text;
            data[1] = ICText.Text;
            data[2] = UIText.Text;
            DPLocal.InsertLocalData(data);
            DPLocal.CloseConnection();
        }

        private void LoadCon_Click(object sender, RoutedEventArgs e)
        {
            DPLocal.ConnectTo(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Connections.mdf;Integrated Security=True");
            String[] data = DPLocal.ReadLocalData();
            DSText.Text = data[0];
            ICText.Text = data[1];
            UIText.Text = data[2];
            DPLocal.CloseConnection();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (IDText.Text != "")
            {
                DPServer.DeleteData(IDText.Text);
                UpdateTable();
                IDText.Text = "";
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (IDText.Text != "") {
                DataEdit dataEdit = new DataEdit();
                dataEdit.ShowDialog();
            }
        }

        public void UpdateTable()
        {
            dataGrid.ItemsSource = DPServer.ReadAllData().AsDataView();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            DSText.Text = "";
            ICText.Text = "";
            UIText.Text = "";
            PText.Password = "";
            DPServer.CloseConnection();
            Connector.IsEnabled = true;
            SaveCon.IsEnabled = false;
            ItemInput.IsEnabled = false;
            DataEditor.IsEnabled = false;
            dataGrid.ItemsSource = null;
        }
    }
}
