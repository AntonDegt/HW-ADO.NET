using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ADO.NET.Entities;
using ADO.NET.CRUD;

namespace ADO.NET
{
    /// <summary>
    /// Interaction logic for DisconnectWindow.xaml
    /// </summary>
    public partial class DisconnectedWindow : Window
    {
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Manager> Managers { get; set; }

        public DisconnectedWindow()
        {
            InitializeComponent();
            // Связывание. Часть 1. Контекст
            DataContext = this;  // Представление получает доступ к всему объекту окна
            SqlConnection connection = new SqlConnection(App.ConnectionString);
            try
            {
                connection.Open();

                #region Departments
                Departments = new ObservableCollection<Department>();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Departments", connection);
                {
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Departments.Add(new Department()  // Изменение коллекции отобразиться на ListView
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
                #endregion

                #region Products
                Products = new ObservableCollection<Product>();
                using SqlCommand cmd2 = new SqlCommand("SELECT Id, Name, Price FROM Products", connection);
                {
                    using var reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        Products.Add(new Product()  // Изменение коллекции отобразиться на ListView
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDouble(2)
                        });
                    }
                }
                #endregion

                #region Managers
                Managers = new ObservableCollection<Manager>();
                using SqlCommand cmd3 = new SqlCommand("SELECT Id, Surname, Name, Secname, Id_main_dep, Id_sec_dep, Id_chief FROM Managers", connection);
                {
                    using var reader = cmd3.ExecuteReader();
                    while (reader.Read())
                    {
                        Guid? a = Guid.Empty;
                        if (reader[5] != DBNull.Value)
                            a = reader.GetGuid(5);

                        Guid? b = Guid.Empty;
                        if (reader[6] != DBNull.Value)
                            reader.GetGuid(6);

                        Managers.Add(new Manager()
                        {
                            Id = reader.GetGuid(0),
                            Surname = reader.GetString(1),
                            Name = reader.GetString(2),
                            Secname = reader.GetString(3),
                            IdMainDep = reader.GetGuid(4),
                            IdSecDep = a,   
                            IdChief =  b   
                        });                                    
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        // Departaments Double Click
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)  // item = sender as ListViewItem
            {
                // Обратная связь (view->model) через item.Content
                if (item.Content is Department department)
                {
                    // MessageBox.Show(department.ToShortString());
                    DepartmentWindow window = new DepartmentWindow()
                    {
                        Department = department
                    };
                    int index = Departments.IndexOf(department);
                    Departments.Remove(department);  // удаляем из коллекции и передаем на редактирование
                    if (window.ShowDialog().GetValueOrDefault())
                    {
                        SqlConnection connection = new SqlConnection(App.ConnectionString);
                        try
                        {
                            connection.Open();
                            using SqlCommand cmd = new SqlCommand() { Connection = connection };
                            if (window.Department is null)  // удаление
                            {
                                cmd.CommandText = $"DELETE FROM Departments WHERE Id = '{department.Id}' ";
                            }
                            else  // изменение
                            {
                                cmd.CommandText = $"UPDATE Departments SET Name = @name WHERE Id = '{department.Id}' ";
                                cmd.Parameters.AddWithValue("@name", department.Name);
                                Departments.Insert(index, department);  // возвращаем, но измененный
                            }
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Задание выполнено успешно");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else  // Отмена - возвращаем в окно
                    {
                        Departments.Insert(index, department);
                    }
                }
            }
        }

        // Products Double Click    !NOT FINISHED!
        private void ListViewItem_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {

                if (item.Content is Entities.Product product)
                {
                    MessageBox.Show(product.ToShortString());
                }
            }
        }

        // Managers Double Click 
        private void ListViewItem_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)  // item = sender as ListViewItem
            {
                // Обратная связь (view->model) через item.Content
                Manager manager = item.Content as Manager;
                if (manager != null)
                {
                    // MessageBox.Show(department.ToShortString());
                    ManagerWindow window = new ManagerWindow(Departments, Managers, manager);
                    int index = Managers.IndexOf(manager);
                    Managers.Remove(manager);  // удаляем из коллекции и передаем на редактирование
                    if (window.ShowDialog().GetValueOrDefault())
                    {
                        SqlConnection connection = new SqlConnection(App.ConnectionString);
                        try
                        {
                            connection.Open();
                            using SqlCommand cmd = new SqlCommand() { Connection = connection };
                            if (window.manager is null)  // удаление
                            {
                                cmd.CommandText = $"DELETE FROM Managers WHERE Id = '{manager.Id}' ";
                            }
                            else  // изменение
                            {
                                cmd.CommandText = $"UPDATE Managers SET Surname = @Surname, Name = @Name, Secname = @Secname, Id_main_dep = @IdMainDep, Id_sec_dep = @IdSecDep, Id_chief = @IdChief WHERE Id = '{manager.Id}' ";
                                
                                cmd.Parameters.AddWithValue("@Surname", manager.Surname);
                                cmd.Parameters.AddWithValue("@Name", manager.Name);
                                cmd.Parameters.AddWithValue("@Secname", manager.Secname);
                                cmd.Parameters.AddWithValue("@IdMainDep", manager.IdMainDep);
                                cmd.Parameters.AddWithValue("@IdSecDep", manager.IdSecDep);
                                cmd.Parameters.AddWithValue("@IdChief", manager.IdChief);

                                Managers.Insert(index, manager);  // возвращаем, но измененный
                            }
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Задание выполнено успешно");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else  // Отмена - возвращаем в окно
                    {
                        Managers.Insert(index, manager);
                    }
                }
            }
        }

        // Add Departament Click
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            var window = new DepartmentWindow();

            if (window.ShowDialog().GetValueOrDefault())
            {
                MessageBox.Show(window.Department.ToShortString());
                SqlConnection connection = new SqlConnection(App.ConnectionString);
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(
                        $"INSERT INTO Departments(Id, Name) VALUES( @id, @name )",
                        connection);
                    cmd.Parameters.AddWithValue("@id", window.Department.Id);
                    cmd.Parameters.AddWithValue("@name", window.Department.Name);
                    cmd.ExecuteNonQuery();

                    Departments.Add(window.Department);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Cancel");
            }
        }

        // Add Manager Click
        private void AddManager_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow managerWindow = new ManagerWindow(Departments, Managers);
            managerWindow.ShowDialog();
        }

        
    }
}