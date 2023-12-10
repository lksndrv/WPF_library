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
using System.Xml.Linq;

namespace Lib
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        XDocument doc;
        private void Button_Click_vhod(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "")
            {
                MessageBox.Show("Введите логин");
            }
            if (passwordBox.Password == "")
            {
                MessageBox.Show("Введите пароль");
            }
            doc = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/authentication.xml");
            XElement root = doc.Element("Authentication");
            foreach (XElement ho in root.Elements("person"))
            {
                if (ho.Element("login").Value == textBoxLogin.Text)
                {
                    if (ho.Element("password").Value == passwordBox.Password)
                    {
                        NavigationWindow window = new NavigationWindow();
                        window.Source = new Uri("Page1.xaml", UriKind.Relative);
                        window.Show();

                        this.Close();
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Неверно введен логин или пароль");
                    }
                }
            }
        }

        //    private void Button_Click_vhod(object sender, RoutedEventArgs e)
        //{
        //    string login = textBoxLogin.Text;
        //    string pass = passwordBox.Password;

        //    if (login.Length >= 1)
        //    {
        //        if (pass.Length >= 1)
	       //     {
        //            if (login == "11" && pass == "11")
        //                {
        //                    NavigationWindow window = new NavigationWindow();
        //                    window.Source = new Uri("Page1.xaml", UriKind.Relative);
        //                    window.Show();
                            
        //                    this.Close();
                        
        //                }
        //            else
        //                {
        //                    MessageBox.Show("Неверный логин или пароль");
        //                }
        //        }
        //        else MessageBox.Show("Введите логин");
        //    }
        //    else MessageBox.Show("Введите пароль");
            
            

            //if (login == "1")
            //    if (pass == "1")
            //    {
            //        NavigationWindow window = new NavigationWindow();
            //        window.Source = new Uri("Page1.xaml", UriKind.Relative);
            //        window.Show();
            //        this.Visibility = Visibility.Hidden;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Ошибка");
            //    }
        
       
    }
}
