using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using TFT_Overlay.Properties;

namespace TFT_Overlay
{
    public partial class App : Application
    {
        private void AutoUpdater(object sender, StartupEventArgs e)
        {
            string currentVersion = Version.version;
            string version;

            using (WebClient client = new WebClient())
            {
                try
                {
                    string htmlCode = client.DownloadString("https://raw.githubusercontent.com/Just2Good/TFT-Overlay/master/Version.cs");
                    int versionFind = htmlCode.IndexOf("public static string version = ");
                    version = htmlCode.Substring(versionFind + 32, 5);
                    if (currentVersion != version && Settings.Default.AutoUpdate)
                    {
                        var result = MessageBox.Show($"A new update is available.\nWould you like to download V{version}?", "TFT Overlay Update Available", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            string link = "https://github.com/Just2good/TFT-Overlay/releases/download/V" + version + "/TFT.Overlay.V" + version + ".zip";
                            ServicePointManager.Expect100Continue = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            client.DownloadFile(new Uri(link), "TFTOverlay.zip");

                            var res = MessageBox.Show("The zip file was downloaded to your local directory, please extract and use the updated version instead.\nDo you want to open your local directory?",
                                "Success", MessageBoxButton.YesNo, MessageBoxImage.Information);
                            if (res == MessageBoxResult.Yes)
                            {
                                Process.Start(Directory.GetCurrentDirectory());
                            }
                        }
                        else if (result == MessageBoxResult.No)
                        {
                            Settings.FindAndUpdate("AutoUpdate", false);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show(ex.ToString(), "An error occured", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}