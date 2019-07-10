using System;
using System.Net;
using System.Windows;
using System.Windows.Forms;

namespace TFT_Overlay
{

    public partial class App : System.Windows.Application
    {
        private void AutoUpdater(object sender, StartupEventArgs e)
        {
            string currentVersion = Version.version;
            string version;
            using (WebClient client = new WebClient())
            {
                
                string htmlCode = client.DownloadString("https://raw.githubusercontent.com/Just2Good/TFT-Overlay/master/Version.cs");
                int versionFind = htmlCode.IndexOf("public static string version = ");
                version = htmlCode.Substring(versionFind + 32,5);
                if (currentVersion != version && TFT_Overlay.Properties.Settings.Default.AutoUpdate)
                {
                    DialogResult result = System.Windows.Forms.MessageBox.Show("New update available, would you like to download it?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            string link = "https://github.com/Just2good/TFT-Overlay/releases/download/V" + version + "/TFT.Overlay.V" + version + ".rar";
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            client.DownloadFile(new Uri(link), "TFTOverlay.zip");
                            System.Windows.Forms.MessageBox.Show("The zip file was downloaded to your local directory, please extract and use the updated version instead.", "Downloaded");
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine(ex.Message);
                            System.Windows.Forms.MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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