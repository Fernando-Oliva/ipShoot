using System;
using System.Windows;
using ipShoot.Shoots;
using System.Windows.Media;


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

        private void btnRemoteShoot_Click(object sender, RoutedEventArgs e)
        {
            if (!txtHostName.Text.Equals(string.Empty))
            {
                txbIpRemote.Text = string.Empty;

                string ipf = string.Empty;

                foreach (var ip in Shoot.GetRemoteIP(txtHostName.Text))
                {
                    txbIpRemote.Text = txbIpRemote.Text + ip.AddressFamily + ": " + ip.ToString() + Environment.NewLine;
                    ipf = ip.ToString();
                }

                // Add initial content to the RichTextBox.
                rtbLocationData.Document = Shoot.GetLocationData(ipf);
                rtbLocationData.Visibility = System.Windows.Visibility.Visible;

                btnRemoteShoot.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                btnRemoteShoot.Margin = new Thickness(100, 0, 0, 0);

                txtHostName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                txtHostName.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                txtHostName.Margin = new Thickness(107, 0, 0, 70);
            }
            else
            {
                MessageBox.Show("Type a host name!");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txbMachineName.Text = Shoot.GetMachineName();

            foreach (var ip in Shoot.GetLocalIPs())
            {
                txbIPNames.Text = txbIPNames.Text + ip.AddressFamily + ": " + ip.ToString() + Environment.NewLine;
            } 
        }

        private void cbConsoleMode_Checked(object sender, RoutedEventArgs e)
        {
            if (cbConsoleMode.IsChecked.Value)
            {
                // ====== CONFIG TAB =================
                configTab.Background = Brushes.Black;
                cbConsoleMode.Foreground = Brushes.LimeGreen;

                // ====== LOCAL TAB =================
                localTab.Background = Brushes.Black;
                txbMachineName.Foreground = Brushes.LimeGreen;
                txbMachineNameLiteral.Foreground = Brushes.LimeGreen;
                txbIPNames.Foreground = Brushes.LimeGreen;
                borderInfo.BorderBrush = Brushes.LimeGreen;

                // ====== REMOTE TAB ================
                remoteTab.Background = Brushes.Black;
                txtHostName.Foreground = Brushes.LimeGreen;
                rtbLocationData.Foreground = Brushes.LimeGreen;
                txbIpRemote.Foreground = Brushes.LimeGreen;
            }
            else
            {
                configTab.Background = Brushes.White;
                cbConsoleMode.Foreground = Brushes.Black;

                // ====== LOCAL TAB =================
                localTab.Background = Brushes.White;
                txbMachineName.Foreground = Brushes.Black;
                txbMachineNameLiteral.Foreground = Brushes.Black;
                txbIPNames.Foreground = Brushes.Black;
                borderInfo.BorderBrush = Brushes.Black;

                // ====== REMOTE TAB ================
                remoteTab.Background = Brushes.White;
                txtHostName.Foreground = Brushes.Black;
                rtbLocationData.Foreground = Brushes.Black;
                txbIpRemote.Foreground = Brushes.Black;
            }
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var sysInfo in Shoot.GetSystemInformation())
            {
                txbSysInfo.Text = txbSysInfo.Text + sysInfo + Environment.NewLine;
            } 
        }
    }
}
