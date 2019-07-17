using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TFT_Overlay.Properties;
using TFT_Overlay.Utilities;

namespace TFT_Overlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Cursor LoLNormal = CustomCursor.FromByteArray(Properties.Resources.LoLNormal);
        private readonly Cursor LoLPointer = CustomCursor.FromByteArray(Properties.Resources.LoLPointer);
        private readonly Cursor LoLHover = CustomCursor.FromByteArray(Properties.Resources.LoLHover);

        private string CurrentVersion { get; } = Utilities.Version.version;
        private bool OnTop { get; set; } = true;

        private bool canDrag;
        public bool CanDrag
        {
            get => canDrag;
            set
            {
                canDrag = value;
                Settings.FindAndUpdate("Lock", !value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadStringResource(Settings.Default.Language);
            this.Cursor = LoLNormal;
            CanDrag = !Settings.Default.Lock;

            if (Settings.Default.AutoDim == true)
            {
                Thread t = new Thread(new ThreadStart(AutoDim))
                {
                    IsBackground = true
                };

                t.Start();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.Save();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TFT Information Overlay V" + CurrentVersion + " by J2GKaze/Jinsoku#4019\n\nDM me on Discord if you have any questions\n\nLast Updated: July 16th, 2019 @ 8:55PM PST", "About");
        }

        private void MenuItem_Click_Credits(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Big thanks to:\nChaoticoz: Lock Window, Always on Top, and Mouseover\nAsemco/Asemco#7390: Adding Origins and Classes\nAthenyx#9406: Designs\nTenebris: Auto-Updater\nOBJECT#3031: Items/Origins/Classes Strings Base\nJpgdev: Readme format\nKbphan\nEerilai\nꙅꙅɘᴎTqAbɘbᴎɘld#1175: Window Position/Size Saving, CPU Threading Fix\nNarcolic#6374: Item Builder\n\nShoutout to:\nAlexander321#7153 for the Discord Nitro Gift!\nAnonymous for Reddit Gold\nu/test01011 for Reddit Gold\n\nmac#0001 & bNatural#0001(Feel free to bug these 2 on Discord) ;)\nShamish#4895 (Make sure you bug this guy a lot)\nDekinosai#7053 (Buy this man tons of drinks)", "Credits");
        }

        private void MenuItem_Click_Lock(object sender, RoutedEventArgs e)
        {
            CanDrag = !CanDrag;
        }

        private void MenuItem_AutoUpdate(object sender, RoutedEventArgs e)
        {
            string state = Settings.Default.AutoUpdate == true ? "OFF" : "ON";

            MessageBoxResult result = MessageBox.Show($"Would you like to turn {state} Auto-Update? This will restart the program.", "Auto-Updater", MessageBoxButton.OKCancel);

            if (result != MessageBoxResult.OK)
            {
                return;
            }

            Settings.FindAndUpdate("AutoUpdate", !Settings.Default.AutoUpdate);

            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_OnTop(object sender, RoutedEventArgs e)
        {
            if (OnTop)
            {
                this.Topmost = false;
                OnTop = false;
            }
            else
            {
                this.Topmost = true;
                OnTop = true;
            }
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Control)sender).Cursor = LoLPointer;
            if (CanDrag)
            {
                this.DragMove();
            }
        }

        private void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((Control)sender).Cursor = LoLNormal;
        }

        private void MainWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Control)sender).Cursor = LoLHover;
        }

        private void MainWindow_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((Control)sender).Cursor = LoLNormal;
        }

        private void AutoDim_Click(object sender, RoutedEventArgs e)
        {
            string state = Settings.Default.AutoDim == true ? "OFF" : "ON";

            MessageBoxResult result = MessageBox.Show($"Would you like to turn {state} Auto-Dim? This will restart the program.", "Auto-Dim", MessageBoxButton.OKCancel);

            if (result != MessageBoxResult.OK)
            {
                return;
            }

            Settings.FindAndUpdate("AutoDim", !Settings.Default.AutoDim);

            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        private void AutoDim()
        {
            while (true)
            {
                if (IsLeagueOrOverlayActive())
                {
                    Dispatcher.BeginInvoke(new ThreadStart(() => App.Current.MainWindow.Opacity = 1));
                }
                else if (!IsLeagueOrOverlayActive())
                {
                    Dispatcher.BeginInvoke(new ThreadStart(() => App.Current.MainWindow.Opacity = .2));
                }

                Thread.Sleep(500);
            }
        }

        private bool IsLeagueOrOverlayActive()
        {
            string currentActiveProcessName = ProcessHelper.GetActiveProcessName();
            return currentActiveProcessName.Contains("League of Legends") || currentActiveProcessName.Contains("TFT Overlay");
        }

        /// <summary>
        /// Removes previous ItemStrings.xaml from MergedDictionaries and adds the one matching locale tag
        /// </summary>
        /// <param name="locale">localization tag</param>
        private void LoadStringResource(string locale)
        {
            try
            {
                var resources = new ResourceDictionary();

                resources.Source = new Uri("pack://application:,,,/Resource/Localization/ItemStrings_" + locale + ".xaml", UriKind.Absolute);

                var current = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                 m => m.Source.OriginalString.EndsWith("ItemStrings_" + locale + ".xaml"));

                if (current != null)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(current);
                }

                Application.Current.Resources.MergedDictionaries.Add(resources);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LoadStringResource("en-US");
            }
        }

        private void Localization_Credits(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("de-DE: Revyn#0969\nes-AR: Oscarinom\nes-MX: Jukai#3434\nfr-FR: Darkneight\nit-IT: BlackTYWhite#0943\nJA: つかぽん＠PKMotion#8731\nPL: Czapson#9774\npt-BR: Bigg#4019\nRU: Jeremy Buttson#2586\nSL: Shokugeki#0012\nvi-VN: GodV759\nzh-CN: nevex#4441\nzh-TW: noheart#6977\n", "Localization Credits");
        }

        /// <summary>
        /// Takes MenuItem, and passes its Header into LoadStringresource()
        /// </summary>
        /// <param name="sender">Should be of type MenuItem</param>
        /// <param name="e"></param>
        private void Localization_Handler(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string tag = menuItem.Header.ToString();
                LoadStringResource(tag);

                Settings.FindAndUpdate("Language", tag);
            }
        }

        private void ResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Reset();
            CanDrag = true;
            LoadStringResource(Settings.Default.Language);
        }

        private void IconOpacityHandler_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string header = menuItem.Header.ToString();
                double opacity = double.Parse(header.Substring(0, header.Length - 1)) / 100;

                Settings.FindAndUpdate("IconOpacity", opacity);
            }
        }

        private void OpenChangelog_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Just2good/TFT-Overlay/blob/master/README.md#version-history");
        }

        private void LocalizationHelp_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Just2good/TFT-Overlay/blob/master/Localization.md");
        }
    }
}
