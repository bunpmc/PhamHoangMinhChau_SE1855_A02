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
    /// Interaction logic for AddOrderDialog.xaml
    /// </summary>
    public partial class AddOrderDialog : Window
    {
        ProductService ps = new ProductService();
        CustomerService cs = new CustomerService();
        EmployeeService es = new EmployeeService();
        OrderDetailService ods = new OrderDetailService();
        OrderService os = new OrderService();
        public AddOrderDialog()
        {
            InitializeComponent();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            if (cs.GetCustomers() != null && cs.GetCustomers().Count > 0)
            {
                cbCustomer.ItemsSource = cs.GetCustomers();
                cbCustomer.DisplayMemberPath = "FullName";
                cbCustomer.SelectedValuePath = "CustomerId";
            }
            else
            {
                MessageBox.Show("No customers found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (ps.GetAllProducts() != null && ps.GetAllProducts().Count > 0)
            {
                cbProduct.ItemsSource = ps.GetAllProducts();
                cbProduct.DisplayMemberPath = "ProductName";
                cbProduct.SelectedValuePath = "ProductId";
            }
            else
            {
                MessageBox.Show("No products found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (es.GetEmployees() != null && es.GetEmployees().Count > 0)
            {
                cbEmployee.ItemsSource = es.GetEmployees();
                cbEmployee.DisplayMemberPath = "FullName";
                cbEmployee.SelectedValuePath = "EmployeeId";
            }
            else
            {
                MessageBox.Show("No employees found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbCustomer.SelectedItem == null || cbProduct.SelectedItem == null || cbEmployee.SelectedItem == null || string.IsNullOrWhiteSpace(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int customerId = (int)cbCustomer.SelectedValue;
                int productId = (int)cbProduct.SelectedValue;
                int employeeId = (int)cbEmployee.SelectedValue;

                DateTime orderDate = dpDate.SelectedDate ?? DateTime.Now;

                decimal price = decimal.Parse(txtPrice.Text);
                short quantity = short.Parse(txtQuantity.Text);
                float discount = float.Parse(txtDiscount.Text);

                Order order = new Order
                {
                    OrderId = 0,
                    CustomerId = customerId,
                    EmployeeId = employeeId,
                    OrderDate = orderDate,
                };

                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    UnitPrice = price,
                    Quantity = quantity,
                    Discount = discount
                };

                if (os.AddOrder(order) && ods.AddOrderDetail(orderDetail))
                {
                    MessageBox.Show("Order detail added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Failed to add order detail.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding order detail: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
