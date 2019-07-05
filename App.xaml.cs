using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Windows.Forms;

namespace TFT_Overlay
{

    public partial class App : System.Windows.Application
    {

        void AutoUpdater(object sender, StartupEventArgs e)
        {
            string currentVersion = "1.8.2";
            string version;
            

            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString("https://raw.githubusercontent.com/Just2good/TFT-Overlay/master/MainWindow.xaml.cs");
                int versionFind = htmlCode.IndexOf("TFT Information Overlay");
                version = htmlCode.Substring(versionFind + 25, 5);
                if (currentVersion != version && TFT_Overlay.Properties.Settings.Default.AutoUpdate)
                {
                    DialogResult result = System.Windows.Forms.MessageBox.Show("New update available, would you like to download it?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        System.Windows.Forms.MessageBox.Show("The zip file was downloaded to your local directory, please extract and use the updated version instead.", "Downloaded");
                        string link = "https://github.com/Just2good/TFT-Overlay/releases/download/V" + version + "/TFT.Overlay.V" + version + ".rar";
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        client.DownloadFile(new Uri(link), "TFT.rar");
                    }
                    else if (result == DialogResult.No)
                    {
                        TFT_Overlay.Properties.Settings.Default.AutoUpdate = false;
                        TFT_Overlay.Properties.Settings.Default.Save();
                    }
                }
            }
        }
    }
}