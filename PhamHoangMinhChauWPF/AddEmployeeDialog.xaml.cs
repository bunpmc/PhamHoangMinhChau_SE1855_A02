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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services;

namespace PhamHoangMinhChauWPF
{
    /// <summary>
    /// Interaction logic for AddEmployeeDialog.xaml
    /// </summary>
    public partial class AddEmployeeDialog : Window
    {
        EmployeeService es = new EmployeeService();
        public AddEmployeeDialog()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtJobTitle.Text) || string.IsNullOrEmpty(txtAddress.Text)
                    || string.IsNullOrEmpty(txtPassword.Password) || string.IsNullOrEmpty(txtUserName.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                List<Employee> existingEmployees = es.GetEmployees();
                if (existingEmployees.Any(emp => emp.UserName.Equals(txtUserName.Text, StringComparison.OrdinalIgnoreCase) && emp.EmployeeId != (string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text))))
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Employee newEmployee = new Employee
                {
                    EmployeeId = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                    Name = txtName.Text,
                    JobTitle = txtJobTitle.Text,
                    Address = txtAddress.Text,
                    Password = txtPassword.Password,
                    HireDate = dpHireDate.SelectedDate ?? DateTime.Now,
                    BirthDate = dpBirthDate.SelectedDate ?? DateTime.Now,
                    UserName = txtUserName.Text
                };

                if (es.AddEmployee(newEmployee))
                {
                    MessageBox.Show("Employee saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Failed to save employee. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
