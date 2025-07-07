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
using Microsoft.EntityFrameworkCore.Diagnostics;
using Services;

namespace PhamHoangMinhChauWPF
{
    /// <summary>
    /// Interaction logic for UpdateEmployeeDialog.xaml
    /// </summary>
    public partial class UpdateEmployeeDialog : Window
    {
        EmployeeService es = new EmployeeService();
        public UpdateEmployeeDialog(Employee employee)
        {
            InitializeComponent();
            LoadData(employee);
        }

        private void LoadData(Employee employee)
        {
            if (employee != null)
            {
                txtId.Text = employee.EmployeeId.ToString();
                txtName.Text = employee.Name;
                txtJobTitle.Text = employee.JobTitle;
                txtAddress.Text = employee.Address;
                txtPassword.Password = employee.Password;
                dpHireDate.SelectedDate = employee.HireDate;
                dpBirthDate.SelectedDate = employee.BirthDate;
                txtUserName.Text = employee.UserName;
                txtId.IsReadOnly = true;
            }
            else
            {
                MessageBox.Show("Invalid employee data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

                if (es.UpdateEmployee(new Employee
                {
                    EmployeeId = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    JobTitle = txtJobTitle.Text,
                    Address = txtAddress.Text,
                    Password = txtPassword.Password,
                    HireDate = dpHireDate.SelectedDate ?? DateTime.Now,
                    BirthDate = dpBirthDate.SelectedDate ?? DateTime.Now,
                    UserName = txtUserName.Text
                }))
                {
                    MessageBox.Show("Employee updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Failed to update employee.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
