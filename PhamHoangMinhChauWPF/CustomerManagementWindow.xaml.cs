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
    /// Interaction logic for CustomerManagementWindow.xaml
    /// </summary>
    public partial class CustomerManagementWindow : Window
    {
        CustomerService cs = new CustomerService();
        InputValidator iv = new InputValidator();
        public CustomerManagementWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<Customer> customers = cs.GetCustomers();
            if (customers != null && customers.Count > 0)
            {
                lvCustomer.ItemsSource = null;
                lvCustomer.ItemsSource = customers;
            }
            else
            {
                MessageBox.Show("No customers found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                lvCustomer.ItemsSource = null;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try {
                int customerId = int.Parse(txtCustomerId_Search.Text);

                    Customer? customer = cs.SearchCustomerById(customerId);
                    if (customer != null)
                    {
                        lvCustomer.SelectedItem = customer;
                        txtCustomerId.Text = customer.CustomerId.ToString();
                        txtContactName.Text = customer.ContactName;
                        txtCompanyName.Text = customer.CompanyName;
                        txtContactTitle.Text = customer.ContactTitle;
                        txtAddress.Text = customer.Address;
                        txtPhone.Text = customer.Phone;
                    }
                    else
                    {
                        MessageBox.Show("Customer not found.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for the customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string contactName = txtContactName.Text.Trim();
                string companyName = txtCompanyName.Text.Trim();
                string contactTitle = txtContactTitle.Text.Trim();
                string address = txtAddress.Text.Trim();
                string phone = txtPhone.Text.Trim();

                if (!iv.isPhoneValidation(phone))
                {
                    MessageBox.Show("Invalid phone number format. Please enter a valid phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if(string.IsNullOrEmpty(contactName) || string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(contactTitle) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Customer newCustomer = new Customer
                {
                    ContactName = contactName,
                    CompanyName = companyName,
                    ContactTitle = contactTitle,
                    Address = address,
                    Phone = phone
                };

                bool isAdded = cs.AddCustomer(newCustomer);
                if (isAdded) {
                    MessageBox.Show("Customer added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int customerId;
                if (int.TryParse(txtCustomerId.Text.Trim(), out customerId))
                {
                    string contactName = txtContactName.Text.Trim();
                    string companyName = txtCompanyName.Text.Trim();
                    string contactTitle = txtContactTitle.Text.Trim();
                    string address = txtAddress.Text.Trim();
                    string phone = txtPhone.Text.Trim();
                    if (!iv.isPhoneValidation(phone))
                    {
                        MessageBox.Show("Invalid phone number format. Please enter a valid phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(contactName) || string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(contactTitle) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
                    {
                        MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    Customer updatedCustomer = new Customer
                    {
                        CustomerId = customerId,
                        ContactName = contactName,
                        CompanyName = companyName,
                        ContactTitle = contactTitle,
                        Address = address,
                        Phone = phone
                    };
                    bool isUpdated = cs.UpdateCustomer(updatedCustomer);
                    if (isUpdated)
                    {
                        MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Customer ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int customerId;
                if (int.TryParse(txtCustomerId.Text.Trim(), out customerId))
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isDeleted = cs.DeleteCustomer(customerId);
                        if (isDeleted)
                        {
                            MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            txtCustomerId.Clear();
                            txtContactName.Clear();
                            txtCompanyName.Clear();
                            txtContactTitle.Clear();
                            txtAddress.Clear();
                            txtPhone.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the customer. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Customer ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try {                 
                txtCustomerId.Clear();
                txtContactName.Clear();
                txtCompanyName.Clear();
                txtContactTitle.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while clearing the fields: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lvCustomer.SelectedItem is Customer selectedCustomer)
                {
                    txtCustomerId.Text = selectedCustomer.CustomerId.ToString();
                    txtContactName.Text = selectedCustomer.ContactName;
                    txtCompanyName.Text = selectedCustomer.CompanyName;
                    txtContactTitle.Text = selectedCustomer.ContactTitle;
                    txtAddress.Text = selectedCustomer.Address;
                    txtPhone.Text = selectedCustomer.Phone;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting the customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
