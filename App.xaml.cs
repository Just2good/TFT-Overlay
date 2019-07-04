using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net;

namespace TFT_Overlay
{

    public partial class App : Application
    {

        void AutoUpdater(object sender, StartupEventArgs e)
        {
            string currentVersion = "1.7.2";
            string version;

            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString("https://raw.githubusercontent.com/Just2good/TFT-Overlay/master/MainWindow.xaml.cs");
                int versionFind = htmlCode.IndexOf("TFT Information Overlay");
                version = htmlCode.Substring(versionFind + 25, 5);

                if (currentVersion != version)
                {

                    System.Windows.Forms.MessageBox.Show("New version downloading to local directory, please unzip and run.");
                    string link = "https://github.com/Just2good/TFT-Overlay/releases/download/V" + version + "/TFT.Overlay.V" + version + ".rar";
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.DownloadFile(new Uri(link), "TFT.rar");
                }
            }
        }
    }
}