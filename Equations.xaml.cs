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

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для Equations.xaml
    /// </summary>
    public partial class Equations : Window
    {

        public Equations()
        {
            InitializeComponent();
        }

        private void DiscrimClick(object sender, RoutedEventArgs e)
        {
            double a, b, c, dis, x1, x2;
            if ((!double.TryParse(TextBoxA.Text, out a)) || (!double.TryParse(TextBoxB.Text, out b)) || (!double.TryParse(TextBoxC.Text, out c)))
            {
                MessageBox.Show("Данные введены не верно");
                return;
            }

            dis = b * b - 4 * a * c;
            if (dis < 0)
            {
                Result.Text = "нет действительных корней";
                return;
            }
            else
            {

                x1 = ((-b + Math.Sqrt(dis)) / (2 * a));
                x2 = ((-b - Math.Sqrt(dis)) / (2 * a));
                Result.Text = $"dis = {b} * {b} - 4 * {a} * {c}\nx1 = (-{b}+Pi)/(2*{a})={x1}\nx2 = (-{b}-Pi)/(2*{a})={x2}";

            }

        }

        private void VietaClick(object sender, RoutedEventArgs e)
        {
            double a, b, c, dis, x1, x2;
            if ((!double.TryParse(TextBoxA.Text, out a)) || (!double.TryParse(TextBoxB.Text, out b)) || (!double.TryParse(TextBoxC.Text, out c)))
            {
                MessageBox.Show("Данные введены не верно");
                return;
            }
            if (a == 0)
            {
                MessageBox.Show("a не должна быть равна нулю");
            }
            x1 = c / b;
            x2 = -c / a;
            Result.Text = $"x1 = {c} / {b}\nx2 = -{c} / {a}\nx1 = {x1}, x2 = {x2}";
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
