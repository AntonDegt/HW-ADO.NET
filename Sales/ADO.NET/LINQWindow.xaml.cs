using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADO.NET
{
    /// <summary>
    /// Логика взаимодействия для LINQWindow.xaml
    /// </summary>
    public partial class LINQWindow : Window
    {
        private LinqContext.DataContext context;
        public LINQWindow()
        {
            InitializeComponent();
            try
            {
                context = new LinqContext.DataContext(App.ConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void All_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            foreach (Entities.Product item in context.Products)
            {
                textBlock1.Text += item.Id + " " + item.Name + " " + item.Price + "\n";
            }
            textBlock1.Text += "\nTotal: " + context.Products.Count();
        }

        private void ByName_Click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderBy(p => p.Name);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Id + " " + item.Name + " " + item.Price + "\n";
            }
            textBlock1.Text += "\nTotal: " + query.Count();
        }

        private void ByPrice_Click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.OrderByDescending(p => p.Price);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Id + " " + item.Name + " " + item.Price + "\n";
            }
            textBlock1.Text += "\nTotal: " + query.Count();
        }

        private void Less200_Click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.Where(p => p.Price < 200);
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Id + " " + item.Name + " " + item.Price + "\n";
            }
            textBlock1.Text += "\nTotal: " + query.Count();
        }

        private void startGendOV_Click(object sender, RoutedEventArgs e)
        {
            var query = context.Products.Where(p => (p.Name.ToLower().StartsWith("г") && p.Name.ToLower().Contains("ов")));
            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Id + " " + item.Name + " " + item.Price + "\n";
            }
            textBlock1.Text += "\nTotal: " + query.Count();
        }

        private void ManagersAndDep_Click(object sender, RoutedEventArgs e)
        {
            var query = context.Managers.Join(
                context.Departments,
                m => m.IdMainDep,
                d => d.Id,
                (m, d) => new { Manager = m.Surname + " " + m.Name, Department = d.Name }
            );

            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Manager + " - " + item.Department + "\n";
            }
            textBlock1.Text += "\nTotal: " + query.Count();
        }

        private void ManagersAndChief_Click(object sender, RoutedEventArgs e)
        {
            var query = from m in context.Managers
                        join c in context.Managers on m.IdChief equals c.Id
                        select new
                        {
                            Manager = m.Surname + " " + m.Name,
                            Chief = c.Surname + " " + c.Name
                        };

            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Manager + " - " + item.Chief + "\n";
            }
            textBlock1.Text += "\nTotal: " + query.Count();
        }

        private void ManagersAndDepAndChief_Click(object sender, RoutedEventArgs e)
        {
            var query = from m in context.Managers
                        join d in context.Departments on m.IdMainDep equals d.Id
                        join c in context.Managers on m.IdChief equals c.Id
                        select new
                         {
                             Manager = m.Surname + " " + m.Name,
                             Department = d.Name,
                             Chief = c.Surname + " " + c.Name
                         };

            textBlock1.Text = "";
            foreach (var item in query)
            {
                textBlock1.Text += item.Manager + " - " + item.Department + " - " + item.Chief + "\n";
            }
            textBlock1.Text += "\nTotal: " + query.Count();
        }
    }
}
