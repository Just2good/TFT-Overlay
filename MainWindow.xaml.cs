using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TFT_Overlay
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Cursor LoLNormal = CustomCursor.FromByteArray(Properties.Resources.LoLNormal);
        private Cursor LoLPointer = CustomCursor.FromByteArray(Properties.Resources.LoLPointer);
        private Cursor LoLHover = CustomCursor.FromByteArray(Properties.Resources.LoLHover);

        private bool OnTop { get; set; } = true;
        private bool CanDrag { get; set; } = true;
        private string CurrentVersion { get; } = Version.version;

        public MainWindow()
        {
            InitializeComponent();
            LoadStringResource("en-US");
            this.Cursor = LoLNormal;
            MouseLeftButtonDown += new MouseButtonEventHandler(MainWindow_MouseLeftButtonDown);
            MouseLeftButtonUp += new MouseButtonEventHandler(MainWindow_MouseLeftButtonUp);
            MouseRightButtonDown += new MouseButtonEventHandler(MainWindow_MouseRightButtonDown);
            MouseRightButtonUp += new MouseButtonEventHandler(MainWindow_MouseRightButtonUp);
            this.Opacity = 1;

            if (Properties.Settings.Default.AutoDim == true)
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
            Properties.Settings.Default.Save();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TFT Information Overlay V" + CurrentVersion + " by J2GKaze/Jinsoku#4019\n\nDM me on Discord if you have any questions\n\nLast Updated: July 4th, 2019 @ 10:47PM PST", "About");
        }

        private void MenuItem_Click_Credits(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Big thanks to:\nChaoticoz for Lock Window, Always on Top, and Mouseover\nAsemco/Asemco#7390 for adding Origins and Classes\nAthenyx#9406 for Designs.\nTenebris for Auto-Updater\nOBJECT#3031 for tons of Item String descriptions\nJpgdev for Readme format\nKbphan\nEerilai\n\nShoutout to:\nAlexander321#7153 for the Discord Nitro Gift!\nAnonymous for Reddit Gold\nu/test01011 for Reddit Gold\n\nmac#0001 & bNatural#0001(Feel free to bug these 2 on Discord) ;)\nShamish#4895 (Make sure you bug this guy a lot)\nDekinosai#7053 (Buy this man tons of drinks)", "Credits");
        }

        private void MenuItem_Click_Lock(object sender, RoutedEventArgs e)
        {
            CanDrag = !CanDrag;
        }

        private void MenuItemAutoUpdate(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AutoUpdate = !Properties.Settings.Default.AutoUpdate;
            Properties.Settings.Default.Save();
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
                Thread.Sleep(100);
            }
        }

        private static bool IsLeagueOrOverlayActive()
        {
            var currentActiveProcessName = ProcessHelper.GetActiveProcessName();
            return currentActiveProcessName.Contains("League of Legends") || currentActiveProcessName.Contains("TFT Overlay");
        }

        private void AutoDim_Click(object sender, RoutedEventArgs e)
        {
            string state = Properties.Settings.Default.AutoDim == true ? "OFF" : "ON";

            var result = MessageBox.Show($"Would you like to turn {state} Auto-Dim? This will restart the program.", "Auto-Dim", MessageBoxButton.OKCancel);

            if (result != MessageBoxResult.OK)
            {
                return;
            }

            Properties.Settings.Default.AutoDim = !Properties.Settings.Default.AutoDim;
            Properties.Settings.Default.Save();
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        private void LoadStringResource(string locale)
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

        private void US_Click(object sender, RoutedEventArgs e)
        {
            LoadStringResource("en-US");
        }

        private void TEST_Click(object sender, RoutedEventArgs e)
        {
            LoadStringResource("en-TEST");
        }
    }
}
