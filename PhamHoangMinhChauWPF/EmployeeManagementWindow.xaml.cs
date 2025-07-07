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
using DataAccessLayer;
using Services;

namespace PhamHoangMinhChauWPF
{
    /// <summary>
    /// Interaction logic for EmployeeManagementWindow.xaml
    /// </summary>
    public partial class EmployeeManagementWindow : Window
    {
        EmployeeService es = new EmployeeService();
        public EmployeeManagementWindow()
        {
            InitializeComponent();
            LoadDataIntoListView();
        }

        private void LoadDataIntoListView()
        {
            List<Employee> employees = es.GetEmployees();
            if (employees != null && employees.Count > 0)
            {
                lvEmployee.ItemsSource = null;
                lvEmployee.ItemsSource = employees;
            }
            else
            {
                MessageBox.Show("No employees found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            if (addEmployeeDialog.ShowDialog() == true)
            {
                LoadDataIntoListView();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployeeDialog updateEmployeeDialog = new UpdateEmployeeDialog((Employee)lvEmployee.SelectedItem);
            if (updateEmployeeDialog.ShowDialog() == true)
            {
                LoadDataIntoListView();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvEmployee.SelectedItem == null)
                {
                    MessageBox.Show("Please select an employee to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Employee selectedEmployee = (Employee)lvEmployee.SelectedItem;
                if (MessageBox.Show($"Are you sure you want to delete the employee '{selectedEmployee.Name}'?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (es.DeleteEmployee(selectedEmployee.EmployeeId))
                    {
                        MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadDataIntoListView();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete employee. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show($"An error occurred while deleting: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
