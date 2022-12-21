using CleanPCClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime dateTime;
        AuthMenu authMenu;
        PC pc;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public DateTime DateTime { get => dateTime; set => dateTime = value; }

        public MainWindow()
        {
            InitializeComponent();
            PC.CheckMAC();
            authMenu = new AuthMenu(this);
            authMenu.Visibility = Visibility.Collapsed;
            dockMenu.Children.Add(authMenu);
            pc = new PC();

            TimerStart();

        }

        private void AnLock_B_Click(object sender, RoutedEventArgs e)
        {
            PC pc = new PC();
            if (pc.CheckStatus() == true)
            {
                this.dateTime = Convert.ToDateTime(Order.SelectFinishTime(pc));
                this.Hide();
            }else
            {
                AnLock_B.Visibility = Visibility.Collapsed;
                authMenu.Visibility = Visibility.Visible;
            }


            }

        public void TimerStart()
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            pc.CheckStatus();
            pc.SetStatusOnline();
            if (this.dateTime <= DateTime.Now || pc.StatusPC == 0)
            {
                this.Visibility = Visibility.Visible;
                pc.UpdateStatus(0);
            }
        }
        public void StartPozition()
        {
            AnLock_B.Visibility = Visibility.Visible;
            authMenu.Visibility = Visibility.Collapsed;
        }
    }
}
