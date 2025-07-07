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
    /// Interaction logic for OrderManagementWindow.xaml
    /// </summary>
    public partial class OrderManagementWindow : Window
    {
        OrderService os = new OrderService();
        OrderDetailService ods = new OrderDetailService();
        public OrderManagementWindow()
        {
            InitializeComponent();
            LoadDataIntoListView();
        }

        private void LoadDataIntoListView()
        {
            try
            {
                List<Order> orders = os.GetOrders();
                if (orders != null && orders.Count > 0)
                {
                    lvOrder.ItemsSource = null;
                    lvOrder.ItemsSource = orders;
                }
                else
                {
                    MessageBox.Show("No orders found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading orders: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrderDialog addOrderDialog = new AddOrderDialog();
            addOrderDialog.ShowDialog();
            if (addOrderDialog.DialogResult == true)
            {
                MessageBox.Show("Order added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDataIntoListView();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvOrder.SelectedItem == null)
            {
                MessageBox.Show("Please select an order to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Order order = (Order) lvOrder.SelectedItem;
            UpdateOrderDialog updateOrderDialog = new UpdateOrderDialog(order);
            updateOrderDialog.ShowDialog();
            if (updateOrderDialog.DialogResult == true)
            {
                MessageBox.Show("Order updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDataIntoListView();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvOrder.SelectedItem is Order selectedOrder)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the order with ID {selectedOrder.OrderId}?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (os.DeleteOrder(selectedOrder))
                        {
                            MessageBox.Show("Order deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadDataIntoListView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete order.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an order to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
