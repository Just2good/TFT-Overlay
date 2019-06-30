using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            MessageBox.Show("TFT Item Combinations Overlay V1.4.1 by J2GKaze/Jinsoku#4019\n\nDM me on Discord if you have any questions\n\nBig thanks to Chaoticoz for fixing Lock Window and Always On Top");
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


