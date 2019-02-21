using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConnieDevIMS
{
    /// <summary>
    /// Interaction logic for DataEdit.xaml
    /// </summary>
    public partial class DataEdit : Window
    {
        public static MainWindow main = Application.Current.Windows[0] as MainWindow;
        String itemNum = main.IDText.Text;
        public DataProvider DPServer = main.DPServer;
        public DataEdit()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            String[] data = DPServer.ReadData(itemNum);
            TText.Text = data[0];
            SDText.Text = data[1];
            LDText.Text = data[2];
            IText.Text = data[3];
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            String[] data = new String[4];
            data[0] = TText.Text;
            data[1] = SDText.Text;
            data[2] = LDText.Text;
            data[3] = IText.Text;
            DPServer.UpdateData(data, itemNum);
            main.UpdateTable();
            main.IDText.Text = "";
            this.Close();
        }
    }
}
