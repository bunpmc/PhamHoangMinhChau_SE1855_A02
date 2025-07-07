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
    /// Interaction logic for LoginCustomerWindow.xaml
    /// </summary>
    public partial class LoginCustomerWindow : Window
    {
        InputValidator iv = new InputValidator();
        CustomerService cs = new CustomerService();
        public LoginCustomerWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                WelcomeWindow welcomeWindow = new WelcomeWindow();
                welcomeWindow.Show();
                Close();
            }
        }

        private void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            string phone = txtPhone.Text;

            try
            {
                if (!iv.isPhoneValidation(phone)) {
                    MessageBox.Show($"Please enter a valid phone number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Customer? customer = cs.Login(phone);

                if (customer != null) {
                    CustomerMainWindow customerMainWindow = new CustomerMainWindow(customer);
                    customerMainWindow.Show();
                    Close();
                } else
                {
                    MessageBox.Show($"There is no account with phone number: {phone}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurs while logging in with {phone}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
