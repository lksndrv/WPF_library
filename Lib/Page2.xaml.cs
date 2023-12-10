using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {


        private void LoadTable()
        {
            doc = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");

            var Books = (from x in doc.Element("Books").Elements("book")
                         orderby x.Element("id").Value
                         select new
                         {
                             ID = x.Element("id").Value,
                             Название = x.Element("title").Value,
                             Автор = x.Element("author").Value,
                             Залог = x.Element("deposit").Value,
                             Прокат = x.Element("rental").Value,
                             Жанр = x.Element("genre").Value,
                         }).ToList();

            kniga = new ObservableCollection<object>(Books);
            dg.ItemsSource = kniga;
        }


        public ObservableCollection<object> kniga;
        XDocument doc;
        public Page2()
        {
            InitializeComponent();

            LoadTable();
        }
        private void Bubo(object sender, RoutedEventArgs e)
        {
            doc.Element("Books").Add(new XElement("book",
                              new XElement("id", textboxID.Text),
                              new XElement("title", textbox1.Text),
                              new XElement("author", textbox2.Text),
                              new XElement("deposit", textbox3.Text),
                              new XElement("rental", textbox4.Text),
                              new XElement("genre", textbox5.Text)));
            doc.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");
            bool IdInt = textboxID.Text.All(char.IsDigit);
            bool DepositInt = textbox3.Text.All(char.IsDigit);
            bool RentalInt = textbox4.Text.All(char.IsDigit);
            if (IdInt == true && DepositInt == true && RentalInt == true)
            {
                kniga.Add(new Kniga { ID = textboxID.Text, Название = textbox1.Text, Автор = textbox2.Text, Залог = textbox3.Text, Прокат = textbox4.Text, Жанр = textbox5.Text });
                LoadTable();
                MessageBox.Show("Новые данные добавлены");
            }
            else
            {
                MessageBox.Show("Неверно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        
        

        private void Del(object sender, RoutedEventArgs e)
        {

            doc = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");
            XElement root = doc.Element("Books");
            foreach (XElement xe in root.Elements("book"))
            {
                if (xe.Element("id").Value == textboxID_Del.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                        // Выполнить действие, если пользователь нажал Да
                        xe.Remove();
                        //root.Elements("book").Remove();
                        doc.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");
                        

                        LoadTable();
                        MessageBox.Show("Данные удалены");
                    }
                        else
                        {
                        // Отменить действие, если пользователь нажал Нет
                        }
                    
                }
            }
        }

        private void Edit_Content(object sender, RoutedEventArgs e)
        {
            doc = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");
            XElement root = doc.Element("Books");
            foreach (XElement xe in root.Elements("book"))
            {
                if (xe.Element("id").Value == textboxID_Edit.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool IdIntNew = ID_New.Text.All(char.IsDigit);
                        bool DepositIntNew = Deposit_New.Text.All(char.IsDigit);
                        bool RentalIntNew = Rental_New.Text.All(char.IsDigit);

                        if (ID_New.Text != "" && ID_New.Text != "ID") 
                        {
                            if (IdIntNew == true)
                            {
                                xe.Element("id").Value = ID_New.Text;
                            }
                            else
                            {
                                MessageBox.Show("Неверно введен ID", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        if (Title_New.Text != "" && Title_New.Text != "Название")
                        {
                            xe.Element("title").Value = Title_New.Text;
                        }
                        if (Author_New.Text != "" && Author_New.Text != "Автор")
                        {
                            xe.Element("author").Value = Author_New.Text;
                        }
                        if (Deposit_New.Text != "" && Deposit_New.Text != "Залог")
                        {
                            if (DepositIntNew == true)
                            {
                                xe.Element("deposit").Value = Deposit_New.Text;
                            }
                            else
                            {
                                MessageBox.Show("Неверно введен Залог", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        if (Rental_New.Text != "" && Rental_New.Text != "Прокат")
                        {
                            if (RentalIntNew == true)
                            {
                                xe.Element("rental").Value = Rental_New.Text;
                            }
                            else
                            {
                                MessageBox.Show("Неверно введен Прокат", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        if (Genre_New.Text != "" && Genre_New.Text != "Жанр")
                        {
                            xe.Element("genre").Value = Genre_New.Text;
                        }
                        doc.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");
                        LoadTable();
                    }

                }
            }
        }


        private void textbox1G(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textbox1.Text == textbox1.Tag.ToString())
            {
                textbox1.Text = "";
            }
        }
        private void textbox1L(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                textbox1.Text = textbox1.Tag.ToString();
            }
        }
        private void textbox2G(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textbox2.Text == textbox2.Tag.ToString())
            {
                textbox2.Text = "";
            }
        }
        private void textbox2L(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textbox2.Text))
            {
                textbox2.Text = textbox2.Tag.ToString();
            }
        }
        private void textbox3G(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textbox3.Text == textbox3.Tag.ToString())
            {
                textbox3.Text = "";
            }
        }
        private void textbox3L(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textbox3.Text))
            {
                textbox3.Text = textbox3.Tag.ToString();
            }
        }
        private void textbox4G(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textbox4.Text == textbox4.Tag.ToString())
            {
                textbox4.Text = "";
            }
        }
        private void textbox4L(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textbox4.Text))
            {
                textbox4.Text = textbox4.Tag.ToString();
            }
        }
        private void textbox5G(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textbox5.Text == textbox5.Tag.ToString())
            {
                textbox5.Text = "";
            }
        }
        private void textbox5L(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textbox5.Text))
            {
                textbox5.Text = textbox5.Tag.ToString();
            }
        }
        private void textboxIDG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxID.Text == textboxID.Tag.ToString())
            {
                textboxID.Text = "";
            }
        }
        private void textboxIDL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxID.Text))
            {
                textboxID.Text = textboxID.Tag.ToString();
            }
        }



        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxID_Del.Text == textboxID_Del.Tag.ToString())
            {
                textboxID_Del.Text = "";
            }
        }
        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxID_Del.Text))
            {
                textboxID_Del.Text = textboxID_Del.Tag.ToString();
            }
        }

        private void id_G(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxID_Edit.Text == textboxID_Edit.Tag.ToString())
            {
                textboxID_Edit.Text = "";
            }
        }
        private void id_L(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxID_Edit.Text))
            {
                textboxID_Edit.Text = textboxID_Edit.Tag.ToString();
            }
        }


        private void id_newG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (ID_New.Text == ID_New.Tag.ToString())
            {
                ID_New.Text = "";
            }
        }
        private void id_newL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(ID_New.Text))
            {
                ID_New.Text = ID_New.Tag.ToString();
            }
        }

        private void title_newG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Title_New.Text == Title_New.Tag.ToString())
            {
                Title_New.Text = "";
            }
        }
        private void title_newL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Title_New.Text))
            {
                Title_New.Text = Title_New.Tag.ToString();
            }
        }

        private void author_newG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Author_New.Text == Author_New.Tag.ToString())
            {
                Author_New.Text = "";
            }
        }
        private void author_newL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Author_New.Text))
            {
                Author_New.Text = Author_New.Tag.ToString();
            }
        }

        private void deposit_newG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Deposit_New.Text == Deposit_New.Tag.ToString())
            {
                Deposit_New.Text = "";
            }
        }
        private void deposit_newL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Deposit_New.Text))
            {
                Deposit_New.Text = Deposit_New.Tag.ToString();
            }
        }

        private void rental_newG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Rental_New.Text == Rental_New.Tag.ToString())
            {
                Rental_New.Text = "";
            }
        }
        private void rental_newL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Rental_New.Text))
            {
                Rental_New.Text = Rental_New.Tag.ToString();
            }
        }

        private void genre_newG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Genre_New.Text == Genre_New.Tag.ToString())
            {
                Genre_New.Text = "";
            }
        }
        private void genre_newL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Genre_New.Text))
            {
                Genre_New.Text = Genre_New.Tag.ToString();
            }
        }


        private void Source_text_IDG(object sender, RoutedEventArgs e)
        {
            if (Source_text_ID.Text == Source_text_ID.Tag.ToString())
            {
                Source_text_ID.Text = "";
            }
        }
        private void Source_text_IDL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_ID.Text))
            {
                Source_text_ID.Text = Source_text_ID.Tag.ToString();
            }
        }

        private void Source_text_TitleG(object sender, RoutedEventArgs e)
        {
            if (Source_text_Title.Text == Source_text_Title.Tag.ToString())
            {
                Source_text_Title.Text = "";
            }
        }
        private void Source_text_TitleL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_Title.Text))
            {
                Source_text_Title.Text = Source_text_Title.Tag.ToString();
            }
        }
        private void Source_text_AuthorG(object sender, RoutedEventArgs e)
        {
            if (Source_text_Author.Text == Source_text_Author.Tag.ToString())
            {
                Source_text_Author.Text = "";
            }
        }
        private void Source_text_AuthorL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_Author.Text))
            {
                Source_text_Author.Text = Source_text_Author.Tag.ToString();
            }
        }
        private void Source_text_GenreG(object sender, RoutedEventArgs e)
        {
            if (Source_text_Genre.Text == Source_text_Genre.Tag.ToString())
            {
                Source_text_Genre.Text = "";
            }
        }
        private void Source_text_GenreL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_Genre.Text))
            {
                Source_text_Genre.Text = Source_text_Genre.Tag.ToString();
            }
        }



        private void SourceID(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc.Element("Books").Elements("book")
                              where (string)x.Element("id") == Source_text_ID.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Название = x.Element("title").Value,
                                  Автор = x.Element("author").Value,
                                  Залог = x.Element("deposit").Value,
                                  Прокат = x.Element("rental").Value,
                                  Жанр = x.Element("genre").Value,
                              }).ToList();
            dg.ItemsSource = booksCount;
        }

        private void SourceTitle(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc.Element("Books").Elements("book")
                              where (string)x.Element("title") == Source_text_Title.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Название = x.Element("title").Value,
                                  Автор = x.Element("author").Value,
                                  Залог = x.Element("deposit").Value,
                                  Прокат = x.Element("rental").Value,
                                  Жанр = x.Element("genre").Value,
                              }).ToList();
            dg.ItemsSource = booksCount;
        }

        private void SourceAuthor(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc.Element("Books").Elements("book")
                              where (string)x.Element("author") == Source_text_Author.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Название = x.Element("title").Value,
                                  Автор = x.Element("author").Value,
                                  Залог = x.Element("deposit").Value,
                                  Прокат = x.Element("rental").Value,
                                  Жанр = x.Element("genre").Value,
                              }).ToList();
            dg.ItemsSource = booksCount;
        }

        private void SourceGenre(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc.Element("Books").Elements("book")
                              where (string)x.Element("genre") == Source_text_Genre.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Название = x.Element("title").Value,
                                  Автор = x.Element("author").Value,
                                  Залог = x.Element("deposit").Value,
                                  Прокат = x.Element("rental").Value,
                                  Жанр = x.Element("genre").Value,
                              }).ToList();
            dg.ItemsSource = booksCount;
        }

        private void SourceAll(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }
    }


    public class Kniga
    {
        public string ID { get; set; }
        public string Название { get; set; }
        public string Автор { get; set; }
        public string Залог { get; set; }
        public string Прокат { get; set; }
        public string Жанр { get; set; }

        //public override string ToString() => $"{Name} - {Age}";
    }
}
