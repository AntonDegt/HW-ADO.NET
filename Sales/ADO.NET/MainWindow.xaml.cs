using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectedButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new ConnectedWindow().ShowDialog();
            this.Show();
        }

        private void DisconnectedButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new DisconnectedWindow().ShowDialog();
            this.Show();
        }
    }
}
