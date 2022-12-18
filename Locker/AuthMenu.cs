using CleanPCClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Locker
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class AuthMenu : UserControl
    {
        MainWindow mainWindowControl;
        DateTime dateTime;

        public AuthMenu()
        {
            InitializeComponent();
        }

        public AuthMenu(MainWindow mainWindow)
        {
            InitializeComponent();
            Hour_CB.SelectedIndex = 0;
            mainWindowControl = mainWindow;
        }

        private void Auth_B_Click(object sender, RoutedEventArgs e)
        {
            if (Client.SearchLoginPass(Login_TB.Text, Pass_PB.Password) == true)
            {
                PC pc = new PC();
                pc.SelectPCID();
                Client client = new Client();
                client.SelectID(Login_TB.Text);
                dateTime = DateTime.Now;
                dateTime = dateTime.AddHours(Hour_CB.SelectedIndex + 1);
                Order order = new Order(pc.Id, client.Id, 1000, DateTime.Now, dateTime, 1);
                order.Insert();
                pc.UpdateStatus(1);
                mainWindowControl.TimerStart(dateTime);
                mainWindowControl.Visibility = Visibility.Collapsed;


            };


        }


        private void Back_B_Click(object sender, RoutedEventArgs e)
        {
            mainWindowControl.StartPozition();
        }
    }
}
