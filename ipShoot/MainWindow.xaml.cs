using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using ipShoot.Shoots;
using System.Text.RegularExpressions;

namespace ipShoot
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShoot_Click(object sender, RoutedEventArgs e)
        {
            txbMachineName.Text = Shoot.GetMachineName();

            foreach (var ip in Shoot.GetLocalIPs())
            {
                txbIPNames.Text = txbIPNames.Text + ip.AddressFamily + ": " + ip.ToString() + Environment.NewLine;
            } 
        }

        private void btnRemoteShoot_Click(object sender, RoutedEventArgs e)
        {
            if (!txtHostName.Text.Equals(string.Empty))
            {
                txbIpRemote.Text = string.Empty;

                foreach (var ip in Shoot.GetRemoteIP(txtHostName.Text))
                {
                    txbIpRemote.Text = txbIpRemote.Text + ip.AddressFamily + ": " + ip.ToString() + Environment.NewLine;
                }
            }
            else
            {
                MessageBox.Show("Type a host name!");
            }
        }
    }
}
