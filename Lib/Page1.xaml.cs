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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lib
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            //MainFrame1.Content = new Page2();
        }

        private void Button_Knigi(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Page2();
        }
        private void Button_Chitateli(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Page3();
        }
        private void Button_Vidannie(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Page4();
        }
        private void Button_logo_back(object sender, RoutedEventArgs e)
        {
           
            //MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            //mainWindow.Visibility = Visibility.Visible;
            //Window win = (Window)this.Parent;
            //win.Close();
        }

    }

    }
