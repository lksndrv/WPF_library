using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Lib
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public bool IsChecked { get; set; }
        public int CheckBoxValue { get; set; }
        public string CheckBoxValueDa { get; set; }
        public string CheckBoxValueNet { get; set; }
        public int CheckBoxValue_Edit { get; set; }


        public Page3()
        {
            InitializeComponent();

            LoadTable();
        }


        private void LoadTable()
        {
            doc2 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");

            var Readers = (from x in doc2.Element("Readers").Elements("reader")
                           orderby x.Element("id").Value
                           select new
                           {
                               ID = x.Element("id").Value,
                               Фамилия = x.Element("surname").Value,
                               Имя = x.Element("name").Value,
                               Отчество = x.Element("patronymic").Value,
                               Адрес = x.Element("address").Value,
                               Телефон = x.Element("telephone").Value,
                               Льгота = x.Element("benefits").Value,
                           }).ToList();

            reader = new ObservableCollection<object>(Readers);
            dg2.ItemsSource = reader;
        }

        public ObservableCollection<object> reader;
        XDocument doc2;

        private void New_reader(object sender, RoutedEventArgs e)
        {
            if (CheckBoxValue == 0)
            {
                doc2.Element("Readers").Add(new XElement("reader",
                              new XElement("id", textboxID_readers.Text),
                              new XElement("surname", textboxSurname_readers.Text),
                              new XElement("name", textboxName_readers.Text),
                              new XElement("patronymic", textboxPatronymic_readers.Text),
                              new XElement("address", textboxAddress_readers.Text),
                              new XElement("telephone", textboxTelephone_readers.Text),
                              new XElement("benefits", CheckBoxValueNet)));
                doc2.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");
                bool TelephoneInt = textboxTelephone_readers.Text.All(char.IsDigit);
                bool IdInt = textboxID_readers.Text.All(char.IsDigit);
                if (TelephoneInt == true && IdInt == true)
                {
                    reader.Add(new Reader { ID = textboxID_readers.Text, Фамилия = textboxSurname_readers.Text, Имя = textboxName_readers.Text, Отчество = textboxPatronymic_readers.Text, Адрес = textboxAddress_readers.Text, Телефон = textboxTelephone_readers.Text, Льгота = CheckBoxValueNet });
                    MessageBox.Show("Новые данные добавлены");
                }
                else
                {
                    MessageBox.Show("Неверно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                doc2.Element("Readers").Add(new XElement("reader",
                              new XElement("id", textboxID_readers.Text),
                              new XElement("surname", textboxSurname_readers.Text),
                              new XElement("name", textboxName_readers.Text),
                              new XElement("patronymic", textboxPatronymic_readers.Text),
                              new XElement("address", textboxAddress_readers.Text),
                              new XElement("telephone", textboxTelephone_readers.Text),
                              new XElement("benefits", CheckBoxValueDa)));
                doc2.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");
                bool TelephoneInt = textboxTelephone_readers.Text.All(char.IsDigit);
                bool IdInt = textboxID_readers.Text.All(char.IsDigit);
                if (TelephoneInt == true && IdInt == true)
                {
                    reader.Add(new Reader { ID = textboxID_readers.Text, Фамилия = textboxSurname_readers.Text, Имя = textboxName_readers.Text, Отчество = textboxPatronymic_readers.Text, Адрес = textboxAddress_readers.Text, Телефон = textboxTelephone_readers.Text, Льгота = CheckBoxValueDa });
                    MessageBox.Show("Новые данные добавлены");
                }
                else
                {
                    MessageBox.Show("Неверно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        private void reader_Del(object sender, RoutedEventArgs e)
        {
            doc2 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");
            XElement root = doc2.Element("Readers");
            foreach (XElement xe in root.Elements("reader"))
            {
                if (xe.Element("id").Value == textboxID_reader_Del.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Выполнить действие, если пользователь нажал Да
                        xe.Remove();
                        //root.Elements("book").Remove();
                        doc2.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");


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

        private void Edit_Content_reader(object sender, RoutedEventArgs e)
        {
            doc2 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");;
            XElement root = doc2.Element("Readers");
            foreach (XElement xe in root.Elements("reader"))
            {
                if (xe.Element("id").Value == textboxID_reader_Edit.Text)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool TelephoneIntNew = Telephone_New_reader.Text.All(char.IsDigit);
                        bool IdIntNew = ID_New_reader.Text.All(char.IsDigit);

                        if (ID_New_reader.Text != "" && ID_New_reader.Text != "ID")
                        {
                            if (IdIntNew == true)
                            {
                                xe.Element("id").Value = ID_New_reader.Text;
                            }
                            else
                            {
                                MessageBox.Show("Неверно введен ID", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        if (Surname_New_reader.Text != "" && Surname_New_reader.Text != "Фамилия")
                        {
                            xe.Element("surname").Value = Surname_New_reader.Text;
                        }
                        if (Name_New_reader.Text != "" && Name_New_reader.Text != "Имя")
                        {
                            xe.Element("name").Value = Name_New_reader.Text;
                        }
                        if (Patronymic_New_reader.Text != "" && Patronymic_New_reader.Text != "Отчество")
                        {
                            xe.Element("patronymic").Value = Patronymic_New_reader.Text;
                        }
                        if (Address_New_reader.Text != "" && Address_New_reader.Text != "Адрес")
                        {
                            xe.Element("address").Value = Address_New_reader.Text;
                        }
                        if (Telephone_New_reader.Text != "" && Telephone_New_reader.Text != "Телефон")
                        {
                            if (TelephoneIntNew == true)
                            {
                                xe.Element("telephone").Value = Telephone_New_reader.Text;
                            }
                            else
                            {
                                MessageBox.Show("Неверно введен Телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        if (CheckBoxValue_Edit == 1)
                        {
                            if (xe.Element("benefits").Value == "Да")
                            {
                                xe.Element("benefits").Value = "Нет";
                            }
                            else
                            {
                                xe.Element("benefits").Value = "Да";
                            }
                            
                        }
                    }
                        doc2.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");
                        LoadTable();
                    

                }
            }
        }


        private void Benefits_Yes(object sender, RoutedEventArgs e)
        {
            CheckBoxValue = 1;
            CheckBoxValueDa = "Да";
        }

        private void Benefits_No(object sender, RoutedEventArgs e)
        {
            CheckBoxValue = 0;
            CheckBoxValueNet = "Нет";
        }


        private void Benefits_Edit_Yes(object sender, RoutedEventArgs e)
        {
            CheckBoxValue_Edit = 1;
        }

        private void Benefits_Edit_No(object sender, RoutedEventArgs e)
        {
            CheckBoxValue_Edit = 0;
        }






        private void textboxID_readersG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxID_readers.Text == textboxID_readers.Tag.ToString())
            {
                textboxID_readers.Text = "";
            }
        }
        private void textboxID_readersL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxID_readers.Text))
            {
                textboxID_readers.Text = textboxID_readers.Tag.ToString();
            }
        }
        private void textboxSurname_readersG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxSurname_readers.Text == textboxSurname_readers.Tag.ToString())
            {
                textboxSurname_readers.Text = "";
            }
        }
        private void textboxSurname_readersL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxSurname_readers.Text))
            {
                textboxSurname_readers.Text = textboxSurname_readers.Tag.ToString();
            }
        }
        private void textboxName_readersG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxName_readers.Text == textboxName_readers.Tag.ToString())
            {
                textboxName_readers.Text = "";
            }
        }
        private void textboxName_readersL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxName_readers.Text))
            {
                textboxName_readers.Text = textboxName_readers.Tag.ToString();
            }
        }
        private void textboxPatronymic_readersG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxPatronymic_readers.Text == textboxPatronymic_readers.Tag.ToString())
            {
                textboxPatronymic_readers.Text = "";
            }
        }
        private void textboxPatronymic_readersL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxPatronymic_readers.Text))
            {
                textboxPatronymic_readers.Text = textboxPatronymic_readers.Tag.ToString();
            }
        }
        private void textboxAddress_readersG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxAddress_readers.Text == textboxAddress_readers.Tag.ToString())
            {
                textboxAddress_readers.Text = "";
            }
        }
        private void textboxAddress_readersL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxAddress_readers.Text))
            {
                textboxAddress_readers.Text = textboxAddress_readers.Tag.ToString();
            }
        }
        private void textboxTelephone_readersG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxTelephone_readers.Text == textboxTelephone_readers.Tag.ToString())
            {
                textboxTelephone_readers.Text = "";
            }
        }
        private void textboxTelephone_readersL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxTelephone_readers.Text))
            {
                textboxTelephone_readers.Text = textboxTelephone_readers.Tag.ToString();
            }
        }









        private void txtSearch_reader_GotFocus(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxID_reader_Del.Text == textboxID_reader_Del.Tag.ToString())
            {
                textboxID_reader_Del.Text = "";
            }
        }
        private void txtSearch_reader_LostFocus(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxID_reader_Del.Text))
            {
                textboxID_reader_Del.Text = textboxID_reader_Del.Tag.ToString();
            }
        }

        private void id_readerG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (textboxID_reader_Edit.Text == textboxID_reader_Edit.Tag.ToString())
            {
                textboxID_reader_Edit.Text = "";
            }
        }
        private void id_readerL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(textboxID_reader_Edit.Text))
            {
                textboxID_reader_Edit.Text = textboxID_reader_Edit.Tag.ToString();
            }
        }


        private void id_new_readerG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (ID_New_reader.Text == ID_New_reader.Tag.ToString())
            {
                ID_New_reader.Text = "";
            }
        }
        private void id_new_readerL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(ID_New_reader.Text))
            {
                ID_New_reader.Text = ID_New_reader.Tag.ToString();
            }
        }

        private void surname_new_readerG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Surname_New_reader.Text == Surname_New_reader.Tag.ToString())
            {
                Surname_New_reader.Text = "";
            }
        }
        private void surname_new_readerL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Surname_New_reader.Text))
            {
                Surname_New_reader.Text = Surname_New_reader.Tag.ToString();
            }
        }

        private void name_new_readerG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Name_New_reader.Text == Name_New_reader.Tag.ToString())
            {
                Name_New_reader.Text = "";
            }
        }
        private void name_new_readerL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Name_New_reader.Text))
            {
                Name_New_reader.Text = Name_New_reader.Tag.ToString();
            }
        }

        private void patronymic_new_readerG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Patronymic_New_reader.Text == Patronymic_New_reader.Tag.ToString())
            {
                Patronymic_New_reader.Text = "";
            }
        }
        private void patronymic_new_readerL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Patronymic_New_reader.Text))
            {
                Patronymic_New_reader.Text = Patronymic_New_reader.Tag.ToString();
            }
        }

        private void address_new_readerG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Address_New_reader.Text == Address_New_reader.Tag.ToString())
            {
                Address_New_reader.Text = "";
            }
        }
        private void address_new_readerL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Address_New_reader.Text))
            {
                Address_New_reader.Text = Address_New_reader.Tag.ToString();
            }
        }

        private void telephone_new_readerG(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox совпадает с текстом по умолчанию, то очищаем его
            if (Telephone_New_reader.Text == Telephone_New_reader.Tag.ToString())
            {
                Telephone_New_reader.Text = "";
            }
        }
        private void telephone_new_readerL(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox пустой, то восстанавливаем текст по умолчанию
            if (string.IsNullOrEmpty(Telephone_New_reader.Text))
            {
                Telephone_New_reader.Text = Telephone_New_reader.Tag.ToString();
            }
        }


        private void Source_text_ID_readersG(object sender, RoutedEventArgs e)
        {
            if (Source_text_ID_readers.Text == Source_text_ID_readers.Tag.ToString())
            {
                Source_text_ID_readers.Text = "";
            }
        }
        private void Source_text_ID_readersL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_ID_readers.Text))
            {
                Source_text_ID_readers.Text = Source_text_ID_readers.Tag.ToString();
            }
        }

        private void Source_text_Surname_readersG(object sender, RoutedEventArgs e)
        {
            if (Source_text_Surname_readers.Text == Source_text_Surname_readers.Tag.ToString())
            {
                Source_text_Surname_readers.Text = "";
            }
        }
        private void Source_text_Surname_readersL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_Surname_readers.Text))
            {
                Source_text_Surname_readers.Text = Source_text_Surname_readers.Tag.ToString();
            }
        }
        private void Source_text_Name_readersG(object sender, RoutedEventArgs e)
        {
            if (Source_text_Name_readers.Text == Source_text_Name_readers.Tag.ToString())
            {
                Source_text_Name_readers.Text = "";
            }
        }
        private void Source_text_Name_readersL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_Name_readers.Text))
            {
                Source_text_Name_readers.Text = Source_text_Name_readers.Tag.ToString();
            }
        }
        private void Source_text_Patronymic_readersG(object sender, RoutedEventArgs e)
        {
            if (Source_text_Patronymic_readers.Text == Source_text_Patronymic_readers.Tag.ToString())
            {
                Source_text_Patronymic_readers.Text = "";
            }
        }
        private void Source_text_Patronymic_readersL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_Patronymic_readers.Text))
            {
                Source_text_Patronymic_readers.Text = Source_text_Patronymic_readers.Tag.ToString();
            }
        }



        private void SourceID_readers(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc2.Element("Readers").Elements("reader")
                              where (string)x.Element("id") == Source_text_ID_readers.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Фамилия = x.Element("surname").Value,
                                  Имя = x.Element("name").Value,
                                  Отчество = x.Element("patronymic").Value,
                                  Адрес = x.Element("address").Value,
                                  Телефон = x.Element("telephone").Value,
                                  Льгота = x.Element("benefits").Value,
                              }).ToList();
            dg2.ItemsSource = booksCount;
        }

        private void SourceSurname_readers(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc2.Element("Readers").Elements("reader")
                              where (string)x.Element("surname") == Source_text_Surname_readers.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Фамилия = x.Element("surname").Value,
                                  Имя = x.Element("name").Value,
                                  Отчество = x.Element("patronymic").Value,
                                  Адрес = x.Element("address").Value,
                                  Телефон = x.Element("telephone").Value,
                                  Льгота = x.Element("benefits").Value,
                              }).ToList();
            dg2.ItemsSource = booksCount;
        }

        private void SourceName_readers(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc2.Element("Readers").Elements("reader")
                              where (string)x.Element("name") == Source_text_Name_readers.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Фамилия = x.Element("surname").Value,
                                  Имя = x.Element("name").Value,
                                  Отчество = x.Element("patronymic").Value,
                                  Адрес = x.Element("address").Value,
                                  Телефон = x.Element("telephone").Value,
                                  Льгота = x.Element("benefits").Value,
                              }).ToList();
            dg2.ItemsSource = booksCount;
        }

        private void Source_Patronymic_readers(object sender, RoutedEventArgs e)
        {
            var booksCount = (from x in doc2.Element("Readers").Elements("reader")
                              where (string)x.Element("patronymic") == Source_text_Patronymic_readers.Text
                              select new
                              {
                                  ID = x.Element("id").Value,
                                  Фамилия = x.Element("surname").Value,
                                  Имя = x.Element("name").Value,
                                  Отчество = x.Element("patronymic").Value,
                                  Адрес = x.Element("address").Value,
                                  Телефон = x.Element("telephone").Value,
                                  Льгота = x.Element("benefits").Value,
                              }).ToList();
            dg2.ItemsSource = booksCount;
        }

        private void SourceAll_readers(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }
    }




    public class Reader
    {
        public string ID { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public string Адрес { get; set; }
        public string Телефон { get; set; }
        public string Льгота { get; set; }

        
        //public override string ToString() => $"{Name} - {Age}";
    }

}

