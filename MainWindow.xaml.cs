using System;
using System.Collections;
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
//using Microsoft.Data.SQLite;
using System.Data;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using System.Xml;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;
using Brush = System.Windows.Media.Brush;
using System.Data.SQLite;

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Button ContentBtn(string title, int id)
        {

            Button btn = new Button();
            var bc = new BrushConverter();
            btn.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            btn.Content = title;
            btn.FontSize = 16;
            btn.Width = 500;
            btn.Height = 25;
            btn.Margin = new Thickness(0, 0, 0, 5);
            btn.Tag = id;
            btn.Click += ShowSubject;
            ListSubject.Children.Add(btn);
            return btn;
        }

      

        public void ContentFill(string title, string text, string img)
        {

            TextBlock Head = new TextBlock();
            Head.FontSize = 23;
            Head.Text = title;
            Head.Name = "Head";
            Head.Margin = new Thickness(0, 0, 0, 10);
            ContentSubject.Children.Add(Head);

            TextBlock Body = new TextBlock();
            Body.FontSize = 14;
            Body.Text = text;
            Body.Name = "Body";
            ContentSubject.Children.Add(Body);

            Image image = new Image()
            {
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Left,
                Source = new BitmapImage(new Uri("/img/" + img, UriKind.Relative)),
                Margin = new Thickness(0, 0, 0, 20)
            };
            ContentSubject.Children.Add(image);

        }

        public MainWindow()
        {
            InitializeComponent();
           
                int count;
                using (var con = new SQLiteConnection("Data Source=database.db;")) 
                {
                    con.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT count (*) FROM articles", con);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read()) 
                            {
                                count = reader.GetInt32(0);
                            }
                        }
                    }
                    con.Close();
                }
                using (var con = new SQLiteConnection("Data Source=database.db;"))
                {
                    con.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT articles.id,(SELECT titles.title FROM titles WHERE titles.idTitle = articles.idTitle) AS title, articles.text ,(SELECT images.image FROM[images] WHERE images.idImg = articles.idImg) AS image FROM [articles]", con);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) 
                        {
                            int i = 1;
                            while (reader.Read()) 
                            {
                                Button btn = ContentBtn(reader.GetString(1), i);
                                i++;
                            }
                        }
                    }
                    con.Close();
                }

        }

        private void ShowSubject(object sender, RoutedEventArgs e)
        {
            ListSubject.Visibility = Visibility.Collapsed;
            ContentSubject.Visibility = Visibility.Visible;
            Back.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Collapsed;
            var Button = (Button)sender;
            using (var con = new SQLiteConnection("Data Source=database.db;"))
            {
                con.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT articles.id,(SELECT titles.title FROM titles WHERE titles.idTitle = articles.idTitle) AS title, articles.text ,(SELECT images.image FROM[images] WHERE images.idImg = articles.idImg) AS image FROM [articles]", con);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int i = 1;
                        while (reader.Read()) 
                        {
                            if (i == int.Parse(Button.Tag.ToString()))
                            {
                                while (ContentSubject.Children.Count > 0)
                                {
                                    ContentSubject.Children.RemoveAt(0);
                                }

                                ContentFill(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                                break;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
                con.Close();
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Back.Visibility = Visibility.Collapsed;
            ListSubject.Visibility = Visibility.Visible;
         while (ContentSubject.Children.Count > 0)
            {
                ContentSubject.Children.RemoveAt(0);
            }
            Buttons.Visibility = Visibility.Visible;
        }

        private void Equation(object sender, RoutedEventArgs e)
        {
            Equations equations = new Equations();
                equations.Show();
        }

        private void Square(object sender, RoutedEventArgs e)
        {
            pic.Visibility = Visibility.Visible;
            ListSubject.Visibility = Visibility.Collapsed;
            Back.Visibility = Visibility.Visible;
            Buttons.Visibility = Visibility.Collapsed;
        }

       
    }
}