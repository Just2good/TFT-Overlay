using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        bool canDrag = true;
        bool onTop = true;
        public readonly static string VersionNumber = "1.7.5";

        public MainWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += new MouseButtonEventHandler(MainWindow_MouseLeftButtonDown);

            WebClient webClient = new WebClient();
            
            /*
            if (!webClient.DownloadString("weblink to text file with version number").Contains(VersionNumber))
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Update Available would you like to update to version" + " NEW VERSION NUMBER " + "?", "TFT_Overlay", MessageBoxButton.YesNo, MessageBoxImage.None))
                {
                    System.Diagnostics.Process.Start("");
                    //https://youtu.be/Bz85Iu1_ajI <-- couldnt finish and didnt get very far but may be of use for
                    //autoupdates but theres definetly a way you can get it to ping Github directly and check
                }
                else
                {

                }
            }
            */

        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("TFT Information Overlay V"+ VersionNumber +" by J2GKaze/Jinsoku#4019.\n\nDM me on Discord if you have any questions.\n\nBig thanks to Chaoticoz for Lock Window, Always on Top, and Mouseover.\n\nAlso thanks to, Asemco/Asemco#7390 for adding Origins and Classes!\n\nLast Updated: July 2nd, 2019 @ 8:22PM PST");
        }

        private void MenuItem_Click_Lock(object sender, RoutedEventArgs e)
        {
            canDrag = !canDrag;
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


