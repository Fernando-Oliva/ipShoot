using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Management;

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

        /// <summary>
        /// Get Location information
        /// </summary>
        /// <param name="ipf"></param>
        /// <returns></returns>
        public static FlowDocument GetLocationData(string ipf)
        {
            XmlDocument doc = new XmlDocument();
            WebClient myclient = new WebClient();
            string str = myclient.DownloadString("http://geomaplookup.net/index.php?kml=true&ip=" + ipf);
            doc.LoadXml(str);

            string nodes = doc.ChildNodes[1].ChildNodes[0].ChildNodes[2].ChildNodes[3].ChildNodes[0].InnerText;

            Regex regex = new Regex("<strong>(.*)</strong>");
            var v = regex.Match(nodes);

            string s = v.Groups[1].ToString();

            Regex regex1 = new Regex("<strong>(.*)</strong>");
            var v1 = regex.Match(s);

            string s1 = v.Groups[0].ToString();

            string s3 = s1.Substring(s1.IndexOf("<strong>"), s1.IndexOf("<small>"));

            string s2 = s3.Replace("<strong>", "").Replace("</strong>", Environment.NewLine).Replace("<br />", Environment.NewLine).Replace("<em>", "").Replace("</em>", "");

            FlowDocument myFlowDoc = new FlowDocument();

            // Add paragraphs to the FlowDocument.
            myFlowDoc.Blocks.Add(new Paragraph(new Run(s2)));

            return myFlowDoc;
        }

        public enum OS { _2000, XP, Vista, _7, _8 }

        public static OS GetOS()
        {
            var version = Environment.OSVersion.Version;
            switch (version.Major)
            {
                case 5:
                    switch (version.Minor)
                    {
                        case 0:
                            return OS._2000;
                        case 1:
                            return OS.XP;
                        case 2:
                            return OS.XP; //could also be Server 2003, Server 2003 R2
                    }
                    break;
                case 6:
                    switch (version.Minor)
                    {
                        case 0:
                            return OS.Vista; //could also be Server 2008
                        case 1:
                            return OS._7; //could also be Server 2008 R2
                        case 2:
                            return OS._8; //could also be Server 20012, Server 2012 R2
                    }
                    break;
            }

            throw new Exception("Strange OS");
        }

        /// <summary>
        /// Get system information
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSystemInformation()
        {
            List<string> sysInfo = new List<string>();

            string OSName = GetOS().ToString();

            sysInfo.Add("MACHINE NAME: " + Environment.MachineName);
            sysInfo.Add("USER NAME: " + Environment.UserName);
            sysInfo.Add("VERISON: " + Environment.Version.ToString());
            sysInfo.Add("USER DOMAIN NAME: " + Environment.UserDomainName);
            sysInfo.Add("PROCESSOR NUMBER: " + Environment.ProcessorCount.ToString());
            sysInfo.Add("OS: Windows" + OSName);
            sysInfo.Add("OS VERSION: " + Environment.OSVersion.VersionString);
            sysInfo.Add("OS VERSION PLATFORM: " + Environment.OSVersion.Platform.ToString());
            sysInfo.Add("SERVICE PACK: " + Environment.OSVersion.ServicePack);

            ManagementClass mgt = new ManagementClass("Win32_Processor");
            ManagementObjectCollection procs = mgt.GetInstances();

            foreach (var item in procs)
            {
                sysInfo.Add("PROCESSOR: " + item.Properties["Name"].Value.ToString());                
            }

            return sysInfo;
        }
    }
}
