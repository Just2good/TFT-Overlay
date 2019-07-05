using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TFT_Overlay
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        
        bool onTop = true;
        bool canDrag = true;
        string currentVersion = Version.version;

        public MainWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += new MouseButtonEventHandler(MainWindow_MouseLeftButtonDown);
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TFT Information Overlay V" + currentVersion + " by J2GKaze/Jinsoku#4019\n\nDM me on Discord if you have any questions\n\nLast Updated: July 4th, 2019 @ 10:47PM PST");
        }
        private void MenuItem_Click_Credits(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Big thanks to:\nChaoticoz for Lock Window, Always on Top, and Mouseover\nAsemco/Asemco#7390 for adding Origins and Classes\nAthenyx#9406 for Designs.\nTenebris for Auto-Updater\nOBJECT#3031 for tons of Item String descriptions\nJpgdev for Readme format\nKbphan\nEerilai\n\nShoutout to:\nAlexander321#7153 for the Discord Nitro Gift!\nmac#0001 & bNatural#0001(Feel free to bug these 2 on Discord) ;)");
        }

        private void MenuItem_Click_Lock(object sender, RoutedEventArgs e)
        {
            canDrag = !canDrag;
        }

        private void MenuItemAutoUpdate(object sender, RoutedEventArgs e)
        {
            TFT_Overlay.Properties.Settings.Default.AutoUpdate = !TFT_Overlay.Properties.Settings.Default.AutoUpdate;
            TFT_Overlay.Properties.Settings.Default.Save();
        }

        private void MenuItem_Click_OnTop(object sender, RoutedEventArgs e)
        {
            if (onTop)
            {
                this.Topmost = false;
                onTop = false;
            }
            else
            {
                this.Topmost = true;
                onTop = true;
            }

        }

        void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (canDrag)
            {
                this.DragMove();
            }
        }
    }
}








