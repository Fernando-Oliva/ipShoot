using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.Windows.Documents;

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
    }
}
