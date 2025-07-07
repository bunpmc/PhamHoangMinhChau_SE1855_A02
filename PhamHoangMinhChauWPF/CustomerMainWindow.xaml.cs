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
    /// Interaction logic for CustomerMainWindow.xaml
    /// </summary>
    public partial class CustomerMainWindow : Window
    {
        OrderService os = new OrderService();
        OrderDetailService ods = new OrderDetailService();
        CustomerService cs = new CustomerService();
        ProductService ps = new ProductService();
        Customer customerInfo = new Customer();
        public CustomerMainWindow(Customer customer)
        {
            InitializeComponent();
            LoadOrderData(customer);
            LoadCustomerInfo(customer);
            customerInfo = customer;
        }

        private void LoadCustomerInfo(Customer customer)
        {
            txtCustomerId.Text = customer.CustomerId.ToString();
            txtCustomerId.IsReadOnly = true;
            txtAddress.Text = customer.Address;
            txtContactName.Text = customer.ContactName;
            txtCompanyName.Text = customer.CompanyName;
            txtPhone.Text = customer.Phone;
            txtContactTitle.Text = customer.ContactTitle;
        }

        private void LoadOrderData(Customer customer)
        {
            try
            {
                List<Order> orders = os.GetOrders();
                List<OrderDetail> orderDetails = ods.GetOrderDetails();

                lvOrder.ItemsSource = (from o in orders
                                       where o.CustomerId == customer.CustomerId
                                       join ods in orderDetails on o.OrderId equals ods.OrderId
                                       select new
                                       {
                                           o.OrderId,
                                           o.OrderDate,
                                           Product = ps.GetProductById(ods.ProductId),
                                           ods.Quantity,
                                           ods.UnitPrice,
                                           ods.Discount
                                       }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurs while loading orders data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            UpdateCustomerProfileWindow updateCustomerProfileWindow = new UpdateCustomerProfileWindow(customerInfo);
            updateCustomerProfileWindow.ShowDialog();
            if(updateCustomerProfileWindow.DialogResult == true)
            {
                MessageBox.Show("Updated Information Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadCustomerInfo(cs.SearchCustomerById(customerInfo.CustomerId));
            }
        }
    }
}
