using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Cursor LoLNormal = CustomCursor.FromByteArray(Properties.Resources.LoLNormal);
        private Cursor LoLPointer = CustomCursor.FromByteArray(Properties.Resources.LoLPointer);
        private Cursor LoLHover = CustomCursor.FromByteArray(Properties.Resources.LoLHover);

        bool onTop = true;
        bool canDrag = true;
        bool isVisible = true; 
        string currentVersion = Version.version;

        public MainWindow()
        {
            InitializeComponent();
            this.Cursor = LoLNormal;
            MouseLeftButtonDown += new MouseButtonEventHandler(MainWindow_MouseLeftButtonDown);
            MouseLeftButtonUp += new MouseButtonEventHandler(MainWindow_MouseLeftButtonUp);
            MouseRightButtonDown += new MouseButtonEventHandler(MainWindow_MouseRightButtonDown);
            MouseRightButtonUp += new MouseButtonEventHandler(MainWindow_MouseRightButtonUp);


            if (Properties.Settings.Default.AutoHide == true)
               {
                   new Thread(new ThreadStart(AutoHide)).Start();
               } 
        } 

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
        
            MessageBox.Show("TFT Information Overlay V" + currentVersion + " by J2GKaze/Jinsoku#4019\n\nDM me on Discord if you have any questions\n\nLast Updated: July 4th, 2019 @ 10:47PM PST");

        }
        private void MenuItem_Click_Credits(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Big thanks to:\nChaoticoz for Lock Window, Always on Top, and Mouseover\nAsemco/Asemco#7390 for adding Origins and Classes\nAthenyx#9406 for Designs.\nTenebris for Auto-Updater\nOBJECT#3031 for tons of Item String descriptions\nJpgdev for Readme format\nKbphan\nEerilai\n\nShoutout to:\nAlexander321#7153 for the Discord Nitro Gift!\nAnonymous for Reddit Gold\nu/test01011 for Reddit Gold\n\nmac#0001 & bNatural#0001(Feel free to bug these 2 on Discord) ;)\nShamish#4895 (Make sure you bug this guy a lot)\nDekinosai#7053 (Buy this man tons of drinks)");
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
            ((Control)sender).Cursor = LoLPointer;
            if (canDrag)
            {
                this.DragMove();
            }
        }
        void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((Control)sender).Cursor = LoLNormal;
        }
        void MainWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((Control)sender).Cursor = LoLHover;
        }
        void MainWindow_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((Control)sender).Cursor = LoLNormal;
        }


           private void AutoHide()
           {
               while (true)
               {
                   if (IsLeagueOrOverlayActive() && !isVisible)
                   {
                       Dispatcher.BeginInvoke(new ThreadStart(() => App.Current.MainWindow.Show()));
                       isVisible = true;
                   }
                   else if (!IsLeagueOrOverlayActive() && isVisible)
                   {
                       Dispatcher.BeginInvoke(new ThreadStart(() => App.Current.MainWindow.Hide()));
                       isVisible = false;
                   }

                   Thread.Sleep(100);
               }
           } 

          private static bool IsLeagueOrOverlayActive()
          {
              var currentActiveProcessName = ProcessHelper.GetActiveProcessName();
              return currentActiveProcessName.Contains("League of Legends") || currentActiveProcessName.Contains("TFT Overlay");
          }  
    }
}








