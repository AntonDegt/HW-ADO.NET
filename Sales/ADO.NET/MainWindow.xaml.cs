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

using System.Data.SqlClient;

namespace ADO.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection _connection;  // объект-подключение к БД
        public MainWindow()
        {
            InitializeComponent();
            // строка подключения - берется из свойств БД (Server Explorer)
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\qultv\source\repos\ADO.NET\ADO.NET\myDataBase.mdf;Integrated Security=True";
            // создание объекта-подключения !! не открывает подключение
            _connection = new SqlConnection(connectionString);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _connection.Open();  // открытие подключения
                MonitorConnection.Content = "Установлено";
                MonitorConnection.Foreground = Brushes.Green;
            }
            catch (SqlException ex)
            {
                MonitorConnection.Content = "Закрыто";
                MonitorConnection.Foreground = Brushes.Red;
                MessageBox.Show(ex.Message);
                this.Close();
            }
            ShowDepartmentsCount();
            ShowManagersCount();
            ShowProductsCount();
            ShowSalesCount();

            ShowDailyStatistics();

            ShowDepartments();
        }

        #region Show Monitor

        /// <summary>
        /// Выводит в таблицу-монитор количество отделов (департаментов) из БД
        /// </summary>
        private void ShowDepartmentsCount()
        {
            String sql = "SELECT COUNT(*) FROM Departments";
            // SqlCommand объект для передачи команд (запросов) к БД.
            // Требует закрытия, поэтому using
            using var cmd = new SqlCommand(sql, _connection);
            // создание объекта не исполняет команду, для этого есть методы ExecuteXxxx
            MonitorDepartments.Content =
                Convert.ToString(
                    cmd.ExecuteScalar()   // выполняет команду и возвращает "верхний-левый" результат
                );
        }

        /// <summary>
        /// Выводит в таблицу-монитор количество Товаров из БД
        /// </summary>
        private void ShowProductsCount()
        {
            String sql = "SELECT COUNT(*) FROM Products";
            using var cmd = new SqlCommand(sql, _connection);
            MonitorProducts.Content = Convert.ToString(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Выводит в таблицу-монитор количество Сотрудников (Менеджеров) из БД
        /// </summary>
        private void ShowManagersCount()
        {
            using SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Managers", _connection);
            MonitorManagers.Content = Convert.ToString(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Выводит в таблицу-монитор количество Продаж (чеков) из БД
        /// </summary>
        private void ShowSalesCount()
        {
            using SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Sales", _connection);
            MonitorSales.Content = Convert.ToString(cmd.ExecuteScalar());
        }
        #endregion


        /// <summary>
        /// Заполняет блок "Статистика за день"
        /// </summary>
        private void ShowDailyStatistics()
        {
            SqlCommand cmd = new SqlCommand()
            {
                Connection = _connection
            };

            // Самый эффективный менеджер
            cmd.CommandText = "SELECT TOP(1) (SELECT CONCAT(Name, ' ', Surname) FROM Managers WHERE ID = ID_manager) FROM (SELECT ID_manager, (SELECT Price FROM Products WHERE ID = ID_product)*Cnt AS Price FROM Sales) temp_table GROUP BY ID_manager ORDER BY SUM(Price) DESC";
            Manager.Content = Convert.ToString(cmd.ExecuteScalar());

            // Самый эффективный отдел
            cmd.CommandText = "SELECT TOP(1) (SELECT Name FROM Departments WHERE ID = Id_dep) FROM (SELECT (SELECT Id_main_dep FROM Managers WHERE ID = ID_manager) AS Id_dep, Cnt FROM Sales) temp_table GROUP BY Id_dep ORDER BY SUM(Cnt) DESC";
            Departament.Content = Convert.ToString(cmd.ExecuteScalar());

            // Самый популярный товар
            cmd.CommandText = "SELECT TOP(1) (SELECT Name FROM Products WHERE ID = ID_product) FROM Sales GROUP BY ID_product ORDER BY SUM(Cnt) DESC";
            Product.Content = Convert.ToString(cmd.ExecuteScalar());

            cmd.Dispose();
        }


        /// <summary>
        /// Заполняет блок "Отделы" - выборка всех данных из таблицы Departments
        /// </summary>
        private void ShowDepartments()
        {
            using SqlCommand cmd = new SqlCommand("SELECT Id, Name, Cnt, Cnt * Price AS All_price FROM (SELECT Id, Name, (SELECT SUM(Cnt) FROM Sales WHERE ID_product = Products.Id AND CAST(Moment AS Date) = CAST(GETDATE() AS Date)) Cnt, Price FROM Products) temp_table WHERE Cnt IS NOT NULL", _connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Guid id = reader.GetGuid(0);
                String name = reader.GetString(1);
                int Cnt = reader.GetInt32(2);
                double All_price = reader.GetDouble(3);
                ProductCell.Content += $"{id} {name} {Cnt} {All_price}\n";
            }
        }

    }
}
