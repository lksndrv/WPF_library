using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {

        public bool IsChecked { get; set; }
        public int Spisana_Check_Value { get; set; }
        public int Problem_Check_Value { get; set; }
        public string Spisana_Check_Value_DA { get; set; }
        public string Spisana_Check_Value_NET { get; set; }
        public string Problem_Check_Value_DA { get; set; }
        public string Problem_Check_Value_NET { get; set; }

        public float Summa {  get; set; }

        public float AllSumma { get; set; }




        public Page4()
        {
            InitializeComponent();

            LoadTable();

        }

        
        

        private void LoadTable()
        {
            doc2 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");
            XElement root2 = doc2.Element("Readers");

            doc = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");
            XElement root1 = doc.Element("Books");

            doc3 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Issued books.xml");
            XElement root3 = doc3.Element("IBooks");

            AllSumma = 0;

            foreach (XElement xe3 in root3.Elements("ibook"))
            {
                foreach (XElement xe1 in root1.Elements("book"))
                {
                    foreach (XElement xe2 in root2.Elements("reader"))
                    {
                        if ((xe3.Element("ibookID").Value == xe1.Element("id").Value) && ((xe3.Element("ireader").Value == xe2.Element("id").Value)))
                        {
                            if (xe3.Element("rdate2").Value != "-")
                            {
                                string idate = xe3.Element("idate").Value;
                                string rdate = xe3.Element("rdate").Value;
                                string rdate2 = xe3.Element("rdate2").Value;
                                DateTime date_i = DateTime.ParseExact(idate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                                DateTime date_r = DateTime.ParseExact(rdate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                                DateTime date_r2 = DateTime.ParseExact(rdate2, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                                if (date_r >= date_r2)
                                {
                                    TimeSpan difference = date_r2 - date_i;
                                    int rent = Int32.Parse(xe1.Element("rental").Value);
                                    if (difference.Days <= 10)
                                    {
                                        Summa = (difference.Days * rent);
                                        Summa = Summa / 100 * 80; //Скидка за раннюю сдачу книги
                                        if (xe3.Element("spisana").Value == "Нет")
                                        {
                                            if (xe3.Element("problem").Value == "Да")
                                            {
                                                int dep = Int32.Parse(xe1.Element("deposit").Value);
                                                Summa = Summa + (dep / 2); 
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Summa = difference.Days * rent;
                                        if (xe3.Element("spisana").Value == "Нет")
                                        {
                                            if (xe3.Element("problem").Value == "Да")
                                            {
                                                int dep = Int32.Parse(xe1.Element("deposit").Value);
                                                Summa = Summa + (dep / 2);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    TimeSpan difference = date_r2 - date_i;
                                    int rent = Int32.Parse(xe1.Element("rental").Value);
                                    Summa = difference.Days * rent;
                                    Summa = Summa / 100 * 120;
                                    if (xe3.Element("spisana").Value == "Нет")
                                    {
                                        if (xe3.Element("problem").Value == "Да")
                                        {
                                            int dep = Int32.Parse(xe1.Element("deposit").Value);
                                            Summa = Summa + (dep / 2);
                                        }
                                    }
                                }
                                if (xe3.Element("spisana").Value == "Да")
                                {
                                    int dep = Int32.Parse(xe1.Element("deposit").Value);
                                    Summa = dep;
                                }
                                if (xe2.Element("benefits").Value == "Да")
                                {
                                    Summa = Summa / 2; //Скидка за льготу
                                }


                                AllSumma += Summa;
                                Allsumma.Text = AllSumma.ToString();

                                xe3.Element("sum").Value = Summa.ToString();
                                doc3.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Issued books.xml");
                            }
                            else
                            {
                                Summa = 0;

                            }
                        }
                    }
                }

            }

            doc3 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Issued books.xml");

            var Ibooks = (from x in doc3.Element("IBooks").Elements("ibook")
                          orderby x.Element("ibookID").Value
                          select new
                          {
                              ID_книги = x.Element("ibookID").Value,
                              ID_читателя = x.Element("ireader").Value,
                              Дата_выдачи = x.Element("idate").Value,
                              Ожидаемая_дата_возврата = x.Element("rdate").Value,
                              Дата_возврата = x.Element("rdate2").Value,
                              Повреждение = x.Element("problem").Value,
                              Списание = x.Element("spisana").Value,
                              Сумма = x.Element("sum").Value,
                          }).ToList();

            ibook = new ObservableCollection<object>(Ibooks);
            dg3.ItemsSource = ibook;

            
        }

        //private void AllS()
        //{
        //    doc3 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Issued books.xml");
        //    XElement root = doc3.Element("IBooks");
        //    float AllSumma = 0;
        //    foreach (XElement x in root.Elements("ibook"))
        //    {
        //        if (x.Element("sum").Value != "" && x.Element("sum").Value != "0")
        //        {
        //            float sum1 = Int32.Parse(x.Element("sum").Value);
        //            AllSumma += sum1;
        //        } 
        //    }
        //    Allsumma.Text = AllSumma.ToString();
        //}

        public ObservableCollection<object> ibook;
        XDocument doc3;
        XDocument doc2;

        XDocument doc;

        private void New_issuance(object sender, RoutedEventArgs e)
        {
            {
                string rdate_txt = "-";
                string prob = "-";
                string spis = "-";
                string summm = "-";
                bool IdInt1 = IBookID.Text.All(char.IsDigit);
                bool IdInt2 = IReaderID.Text.All(char.IsDigit);

                doc2 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Readers.xml");
                XElement root2 = doc2.Element("Readers");

                doc = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Books.xml");
                XElement root1 = doc.Element("Books");

                foreach (XElement xe1 in root1.Elements("book"))
                {
                    if (xe1.Element("id").Value == IBookID.Text)
                    {
                        foreach (XElement xe2 in root2.Elements("reader"))
                        {
                            if (xe2.Element("id").Value == IReaderID.Text)
                            {
                                if (IdInt1 == true && IdInt2 == true)
                                {
                                    doc3.Element("IBooks").Add(new XElement("ibook",
                                              new XElement("ibookID", IBookID.Text),
                                              new XElement("ireader", IReaderID.Text),
                                              new XElement("idate", Idate.Text),
                                              new XElement("rdate", Rdate.Text),
                                              new XElement("rdate2", rdate_txt),
                                              new XElement("problem", prob),
                                              new XElement("spisana", spis),
                                              new XElement("sum", summm)));
                                    doc3.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Issued books.xml");
                                    ibook.Add(new Ibook
                                    {
                                        ID_книги = IBookID.Text,
                                        ID_читателя = IReaderID.Text,
                                        Дата_выдачи = Idate.Text,
                                        Ожидаемая_дата_возврата = Rdate.Text,
                                        Дата_возврата = rdate_txt,
                                        Повреждение = prob,
                                        Списание = spis,
                                        Сумма = summm,
                                    });
                                    LoadTable();
                                    MessageBox.Show("Новые данные добавлены");
                                }
                                else
                                {
                                    MessageBox.Show("Неверно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                    }
                }


            }
        }


        //private void New_issuance(object sender, RoutedEventArgs e)
        //{

        //    string new_date = Rdate.Text;
        //    string new_date2 = IReaderID.Text;

        //    DateTime date = DateTime.ParseExact(new_date, "dd.MM.yyyy", CultureInfo.InvariantCulture);



        //    DateTime date2 = DateTime.ParseExact(new_date2, "dd.MM.yyyy", CultureInfo.InvariantCulture);

        //    if (date > date2)
        //    {
        //        MessageBox.Show("ура");
        //    }
        //    if (date.Day == date2.Day)
        //    {
        //        MessageBox.Show("ура день");
        //    }

        //    //    DateTime? date = datePicker1.SelectedDate;
        //    //    if (date != null)
        //    //    {
        //    //        textblock1.Text = date.Value.ToString("dd.MM.yyyy");
        //    //    }
        //    //    else
        //    //    {
        //    //        textblock1.Text = "Дата не выбрана";
        //    //    }
        //}

        private void New_return(object sender, RoutedEventArgs e)
        {
            doc3 = XDocument.Load("C:/Users/Kotleta/source/repos/Lib/Lib/Issued books.xml"); ;
            XElement root = doc3.Element("IBooks");
            foreach (XElement xe in root.Elements("ibook"))
            {
                if (xe.Element("ibookID").Value == IBookID_return.Text)
                {
                    xe.Element("rdate2").Value = Rdate2_return.Text;
                    if (Problem_Check_Value == 1)
                    {
                        xe.Element("problem").Value = "Да";
                    }
                    else
                    {
                        xe.Element("problem").Value = "Нет";
                    }
                    if (Spisana_Check_Value == 1)
                    {
                        xe.Element("spisana").Value = "Да";
                        xe.Element("problem").Value = "-";
                    }
                    else
                    {
                        xe.Element("spisana").Value = "Нет";
                    }
                }
            }
            doc3.Save("C:/Users/Kotleta/source/repos/Lib/Lib/Issued books.xml");
            LoadTable();
        }



        private void Source_ID_ibook(object sender, RoutedEventArgs e)
        {
            try
            {
                var ibooksCount = (from x in doc3.Element("IBooks").Elements("ibook")
                                   where (string)x.Element("ibookID") == Source_text_ID_ibook.Text
                                   select new
                                   {
                                       ID_книги = x.Element("ibookID").Value,
                                       ID_читателя = x.Element("ireader").Value,
                                       Дата_выдачи = x.Element("idate").Value,
                                       Ожидаемая_дата_возврата = x.Element("rdate").Value,
                                       Дата_возврата = x.Element("rdate2").Value,
                                       Повреждение = x.Element("problem").Value,
                                       Списание = x.Element("spisana").Value,
                                       Сумма = x.Element("sum").Value,
                                   }).ToList();
                dg3.ItemsSource = ibooksCount;
            }
            catch (Exception eee) { MessageBox.Show(eee.ToString()); }


        }

        private void Source_ID_readers(object sender, RoutedEventArgs e)
        {
            var ibooksCount = (from x in doc3.Element("IBooks").Elements("ibook")
                               where (string)x.Element("ireader") == Source_text_ID_reader.Text
                               select new
                               {
                                   ID_книги = x.Element("ibookID").Value,
                                   ID_читателя = x.Element("ireader").Value,
                                   Дата_выдачи = x.Element("idate").Value,
                                   Ожидаемая_дата_возврата = x.Element("rdate").Value,
                                   Дата_возврата = x.Element("rdate2").Value,
                                   Повреждение = x.Element("problem").Value,
                                   Списание = x.Element("spisana").Value,
                                   Сумма = x.Element("sum").Value,
                               }).ToList();
            dg3.ItemsSource = ibooksCount;
        }

        private void Source_All_ibooks(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }



        private void Spisana_Check_Yes(object sender, RoutedEventArgs e)
        {
            Spisana_Check_Value = 1;
            Spisana_Check_Value_DA = "Да";
        }

        private void Spisana_Check_No(object sender, RoutedEventArgs e)
        {
            Spisana_Check_Value = 0;
            Spisana_Check_Value_NET = "Нет";
        }

        private void Problem_Check_Yes(object sender, RoutedEventArgs e)
        {
            Problem_Check_Value = 1;
            Problem_Check_Value_DA = "Да";
        }

        private void Problem_Check_No(object sender, RoutedEventArgs e)
        {
            Problem_Check_Value = 0;
            Problem_Check_Value_NET = "Нет";
        }






        private void IBookIDG(object sender, RoutedEventArgs e)
        {
            if (IBookID.Text == IBookID.Tag.ToString())
            {
                IBookID.Text = "";
            }
        }
        private void IBookIDL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(IBookID.Text))
            {
                IBookID.Text = IBookID.Tag.ToString();
            }
        }
        private void IReaderIDG(object sender, RoutedEventArgs e)
        {
            if (IReaderID.Text == IReaderID.Tag.ToString())
            {
                IReaderID.Text = "";
            }
        }
        private void IReaderIDL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(IReaderID.Text))
            {
                IReaderID.Text = IReaderID.Tag.ToString();
            }
        }
        private void RdateG(object sender, RoutedEventArgs e)
        {
            if (Rdate.Text == Rdate.Tag.ToString())
            {
                Rdate.Text = "";
            }
        }
        private void RdateL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Rdate.Text))
            {
                Rdate.Text = Rdate.Tag.ToString();
            }
        }
        private void IdateG(object sender, RoutedEventArgs e)
        {
            if (Idate.Text == Idate.Tag.ToString())
            {
                Idate.Text = "";
            }
        }
        private void IdateL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Idate.Text))
            {
                Idate.Text = Idate.Tag.ToString();
            }
        }
        private void IBookID_returnG(object sender, RoutedEventArgs e)
        {
            if (IBookID_return.Text == IBookID_return.Tag.ToString())
            {
                IBookID_return.Text = "";
            }
        }
        private void IBookID_returnL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(IBookID_return.Text))
            {
                IBookID_return.Text = IBookID_return.Tag.ToString();
            }
        }
        private void Rdate2_returnG(object sender, RoutedEventArgs e)
        {
            if (Rdate2_return.Text == Rdate2_return.Tag.ToString())
            {
                Rdate2_return.Text = "";
            }
        }
        private void Rdate2_returnL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Rdate2_return.Text))
            {
                Rdate2_return.Text = Rdate2_return.Tag.ToString();
            }
        }
        private void Source_text_ID_ibookG(object sender, RoutedEventArgs e)
        {
            if (Source_text_ID_ibook.Text == Source_text_ID_ibook.Tag.ToString())
            {
                Source_text_ID_ibook.Text = "";
            }
        }
        private void Source_text_ID_ibookL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_ID_ibook.Text))
            {
                Source_text_ID_ibook.Text = Source_text_ID_ibook.Tag.ToString();
            }
        }
        private void Source_text_ID_readerG(object sender, RoutedEventArgs e)
        {
            if (Source_text_ID_reader.Text == Source_text_ID_reader.Tag.ToString())
            {
                Source_text_ID_reader.Text = "";
            }
        }
        private void Source_text_ID_readerL(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source_text_ID_reader.Text))
            {
                Source_text_ID_reader.Text = Source_text_ID_reader.Tag.ToString();
            }
        }
    }

    public class Ibook
    {
        public string ID_книги { get; set; }
        public string ID_читателя { get; set; }
        public string Дата_выдачи { get; set; }
        public string Ожидаемая_дата_возврата { get; set; }
        public string Дата_возврата { get; set; }
        public string Повреждение { get; set; }
        public string Списание { get; set; }
        public string Сумма { get; set; }
    }
}
