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
using System.Data.SqlClient;

namespace Store
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            bool manager = false;
            if (LoginBox.Text.Length > 0 && PasswordBox.Password.Length > 0)
            {
                // Connection
                SqlConnection connection = new SqlConnection(Data.connectionString);
                connection.Open();


                // Products
                SqlCommand command = new SqlCommand($"SELECT id, manager FROM Users WHERE login = '{ LoginBox.Text }' AND password = '{ PasswordBox.Password }'",
                    connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Data.UserID = reader.GetGuid(0);
                    manager = reader.GetBoolean(1);
                }

                connection.Close();

                // Enter
                if (Data.UserID != Guid.Empty)
                {
                    if (manager) 
                        new ManagerWindow().Show();
                    else 
                        new MainWindow().Show();
                    Close();
                }
                else
                    MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void SignUpButton(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().ShowDialog();
        }
    }
}
