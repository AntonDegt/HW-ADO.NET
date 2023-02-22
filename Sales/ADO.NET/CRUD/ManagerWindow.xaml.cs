using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ADO.NET.Entities;

namespace ADO.NET.CRUD
{
    /// <summary>
    /// Interaction logic for CrudManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Manager> Managers { get; set; }

        public Manager manager { get; set; } = null!;

        public ManagerWindow() : this(null, null) { }
        public ManagerWindow(ObservableCollection<Department> Departments, ObservableCollection<Manager> Managers) : this(Departments, Managers, null) { }
        public ManagerWindow(ObservableCollection<Department> Departments, ObservableCollection<Manager> Managers, Manager manager)
        {
            InitializeComponent();
            DataContext = this;

            Departments.Add(new Department() { Id = Guid.NewGuid(), Name = "Test" });

            this.Departments = Departments;
            this.Managers = Managers;
            this.manager = manager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (manager is null)
            {
                manager = new Manager()
                {
                    Id = Guid.NewGuid()
                };
                ButtonDelete.IsEnabled = false;
            }
            else
            {
                ButtonDelete.IsEnabled = true;
                DepartmentsCombo.SelectedValue = manager.IdMainDep;
                SecondaryCombo.SelectedValue = manager.IdSecDep;
                ChiefCombo.SelectedValue = manager.IdChief;
            }
            ManagerId.Text = manager.Id.ToString();

            ManagerSurname.Text = manager.Surname;
            ManagerName.Text = manager.Name;
            ManagerSecname.Text = manager.Secname;
        }


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ManagerSurname.Text.Length > 0)
                if (ManagerName.Text.Length > 0)
                    if (ManagerSecname.Text.Length > 0)
                        if (DepartmentsCombo.SelectedValue is Department department)
                        {
                            Guid id = department.Id;
                            MessageBox.Show(
                                $"{ManagerSurname.Text} - {ManagerName.Text} - {ManagerSecname.Text} - {id}"
                            );
                        }
                        else MessageBox.Show("Выберите отдел");
                    else MessageBox.Show("Введите Отчество");
                else MessageBox.Show("Введите имя");
            else MessageBox.Show("Введите фамилию");

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes ==
                    MessageBox.Show(
                        "Вы подтверждаете удаление Менеджера из БД?",
                        "Удаление данных из БД",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question))
            {
                manager = null!;
                this.DialogResult = true;
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ClearSecondry(object sender, RoutedEventArgs e)
        {
            SecondaryCombo.SelectedItem = null;
        }
        private void ClearChief(object sender, RoutedEventArgs e)
        {
            ChiefCombo.SelectedItem = null;
        }
    }
}