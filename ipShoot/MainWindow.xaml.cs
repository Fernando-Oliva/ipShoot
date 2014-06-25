﻿using System;
using System.Windows;
using ipShoot.Shoots;

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
    }
}
