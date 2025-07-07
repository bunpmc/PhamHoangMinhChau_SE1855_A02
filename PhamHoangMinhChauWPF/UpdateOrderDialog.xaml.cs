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
    /// Interaction logic for UpdateOrderDialog.xaml
    /// </summary>
    public partial class UpdateOrderDialog : Window
    {
        OrderService os = new OrderService();
        OrderDetailService ods = new OrderDetailService();
        ProductService ps = new ProductService();
        CustomerService cs = new CustomerService();
        EmployeeService es = new EmployeeService();
        public UpdateOrderDialog(Order order)
        {
            InitializeComponent();
            LoadComboBox();
            LoadData(order);
        }

        private void LoadData(Order order)
        {
            if (order != null)
            {
                txtId.Text = order.OrderId.ToString();
                cbCustomer.SelectedValue = order.CustomerId;
                cbEmployee.SelectedValue = order.EmployeeId;
                dpDate.SelectedDate = order.OrderDate;
                var orderDetails = ods.GetOrderDetailsByOrderId(order.OrderId);
                if (orderDetails != null && orderDetails.Count > 0)
                {
                    var detail = orderDetails.FirstOrDefault();
                    if (detail != null)
                    {
                        cbProduct.SelectedValue = detail.ProductId;
                        txtQuantity.Text = detail.Quantity.ToString();
                        txtPrice.Text = detail.UnitPrice.ToString("F2");
                        txtDiscount.Text = detail.Discount.ToString("F2");
                    }
                }
                txtId.IsReadOnly = true;
            }
            else
            {
                MessageBox.Show("Invalid order data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(cbCustomer.Text) ||
                   string.IsNullOrWhiteSpace(cbEmployee.Text) || dpDate.SelectedDate == null)
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int orderId = int.Parse(txtId.Text);
                int customerId = (int)cbCustomer.SelectedValue;
                int employeeId = (int)cbEmployee.SelectedValue;
                DateTime orderDate = dpDate.SelectedDate.Value;
                int productId = (int)cbProduct.SelectedValue;
                int quantity = int.Parse(txtQuantity.Text);
                decimal unitPrice = decimal.Parse(txtPrice.Text);
                float discount = float.Parse(txtDiscount.Text);

                if (quantity <= 0 || unitPrice <= 0)
                {
                    MessageBox.Show("Quantity and Price must be greater than zero.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Order order = new Order
                {
                    OrderId = orderId,
                    CustomerId = customerId,
                    EmployeeId = employeeId,
                    OrderDate = orderDate
                };

                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = productId,
                    UnitPrice = unitPrice,
                    Quantity = (short)quantity,
                    Discount = discount
                };

                if (os.UpdateOrder(order) && ods.UpdateOrderDetail(orderDetail))
                {
                    MessageBox.Show("Order updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Failed to update order.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
