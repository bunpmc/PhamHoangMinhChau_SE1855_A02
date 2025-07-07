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
    /// Interaction logic for UpdateCustomerProfileWindow.xaml
    /// </summary>
    public partial class UpdateCustomerProfileWindow : Window
    {
        CustomerService cs = new CustomerService();
        InputValidator iv = new InputValidator();
        public UpdateCustomerProfileWindow(Customer customer)
        {
            InitializeComponent();
            LoadCustomerData(customer);
        }

        private void LoadCustomerData(Customer customer)
        {
            txtCustomerId.Text = customer.CustomerId.ToString();
            txtCustomerId.IsReadOnly = true;
            txtAddress.Text = customer.Address;
            txtContactName.Text = customer.ContactName;
            txtCompanyName.Text = customer.CompanyName;
            txtPhone.Text = customer.Phone;
            txtContactTitle.Text = customer.ContactTitle;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!iv.isPhoneValidation(txtPhone.Text))
                {
                    MessageBox.Show("Please enter a valid phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Customer customer = new Customer() { 
                    CustomerId = int.Parse(txtCustomerId.Text),
                    Address = txtAddress.Text,
                    ContactName = txtContactName.Text,
                    CompanyName = txtCompanyName.Text,
                    Phone = txtPhone.Text,
                    ContactTitle = txtContactTitle.Text,
                };

                if (cs.UpdateCustomer(customer))
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurs while updating profile: {ex.Message}","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
