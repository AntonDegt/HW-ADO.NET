﻿using System;
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
            // В БД информация за 2022 год, поэтому формируем дату с текущим днем и месяцем, но за 2022 год
            String date = $"2022-{DateTime.Now.Month}-{DateTime.Now.Day}";

            // Всего продаж (чеков)
            cmd.CommandText = $"SELECT COUNT(*) FROM Sales S WHERE CAST( S.Moment AS DATE ) = '{date}'";
            StatTotalSales.Content = Convert.ToString(cmd.ExecuteScalar());

            // Всего продаж (товаров, штук)
            cmd.CommandText = $"SELECT SUM(S.Cnt) FROM Sales S WHERE CAST( S.Moment AS DATE ) = '{date}'";
            StatTotalProducts.Content = Convert.ToString(cmd.ExecuteScalar());

            // Всего продаж (грн, деньги)
            cmd.CommandText = $"SELECT ROUND( SUM( S.Cnt * P.Price ), 2 ) FROM Sales S JOIN Products P ON S.Id_product = P.Id WHERE CAST( S.Moment AS DATE ) = '{date}'";
            StatTotalMoney.Content = Convert.ToString(cmd.ExecuteScalar());
            // File.ReadAllText("")

            cmd.Dispose();
        }


        /// <summary>
        /// Заполняет блок "Отделы" - выборка всех данных из таблицы Departments
        /// </summary>
        private void ShowDepartments()
        {
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Departments", _connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Получение элементов записи разными способами
                Guid id = reader.GetGuid(0); // Через метод (id)
                String name = (string) reader[1]; // Через индексатов (id)
                DepartmentCell.Content += $"{id} \n{name} \n";
            }
        }

    }
}