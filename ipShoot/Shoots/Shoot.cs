using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace ipShoot.Shoots
{
    public static class Shoot
    {
        /// <summary>
        /// Get the local machine name
        /// </summary>        
        /// <returns>string machineName</returns>
        public static string GetMachineName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        /// Get local IPs
        /// </summary>
        /// <returns></returns>
        public static List<IPAddress> GetLocalIPs()
        {
            System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();

            List<IPAddress> ips = new List<IPAddress>();

            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in heserver.AddressList)
            {
                ips.Add(ip);
            }

            return ips;
        }

        /// <summary>
        /// Get IP of specified host
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns></returns>
        public static List<IPAddress> GetRemoteIP(string hostName)
        {
            try
            {
                IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);

                return ipaddress.ToList();
            }
            catch (Exception ex)
            {
                List<IPAddress> ipaddress = new List<IPAddress>();

                return ipaddress;
            }            
        }
    }
}
