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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private SqlDataReader GetUsersWhere(SqlConnection connection, Guid id, string login, string email, string phone)
        {
            return new SqlCommand($"SELECT id, login, email FROM Users WHERE id = '{ id.ToString() }' OR login = '{ login }' OR email = '{ email }'", connection).ExecuteReader();
        }

        private void InsertUser(SqlConnection connection, Guid id, string login, string password, string email, string phone)
        {
            SqlCommand cmd_insert = new SqlCommand($"INSERT INTO Users (id, login, password, email, phone, manager) VALUES ('{ id.ToString() }', '{ login }', '{ password }', '{ email }', '{ phone }', 0)", connection);
            cmd_insert.ExecuteNonQuery();
        }


        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length > 0 && PasswordBox.Text.Length > 0 &&
                EmailBox.Text.Length > 0 && PhoneBox.Text.Length > 0)
            {
                SqlConnection connection = new SqlConnection(App.connectionString);

                try
                {
                    connection.Open();

                    Guid id = Guid.Empty;
                    do
                    {
                        id = Guid.NewGuid();
                        
                        SqlDataReader reader = GetUsersWhere(connection, id, LoginBox.Text, EmailBox.Text, PhoneBox.Text);
                        while (reader.Read())
                        {
                            if (id == reader.GetGuid(0))
                            { id = Guid.Empty; break; }

                            if (LoginBox.Text == reader.GetString(1))
                                MessageBox.Show("Логин уже используеться.");
                            if (EmailBox.Text == reader.GetString(2))
                                MessageBox.Show("Почта уже используеться.");


                            reader.Close();
                            connection.Close();
                            return;
                        }
                        reader.Close();
                    }
                    while (id == Guid.Empty);

                    InsertUser(connection, id, LoginBox.Text, PasswordBox.Text, EmailBox.Text, PhoneBox.Text);
                    connection.Close();

                    MessageBox.Show($"Пользователь { LoginBox.Text } успешно зарегестрирован");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
