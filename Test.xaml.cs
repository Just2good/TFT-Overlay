using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Test_Radio
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        bool canDrag = true;
        public Test()
        {
            InitializeComponent();
            MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainWindow_MouseLeftButtonDown);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TFT Item Combinations Overlay V1.0\nReport any bugs to Jinsoku#4019");
        }

        private void MenuItem_Click_OnTop(object sender, RoutedEventArgs e)
        {
            if (AoT.IsChecked)
            {
                this.Topmost = true;
            }
            else
            {
                this.Topmost = false;
            }
        }

        void MainWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (canDrag)
            {
                this.DragMove();
            }
        }

        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            if (Lock.IsChecked)
            {
                canDrag = false;
            }
            else
            {
                canDrag = true;
            }
        }
    }

}
