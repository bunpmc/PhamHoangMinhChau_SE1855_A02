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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        OrderService os = new OrderService();
        OrderDetailService ods = new OrderDetailService();
        public ReportWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (dpEndDate == null || dpStartDate == null) {
                   
                    CalculateSales(dpStartDate?.SelectedDate, dpEndDate?.SelectedDate);
                }
            }catch (Exception ex)
            {
                
                return;
            }
        }

        private void CalculateSales(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                List<Order> orders = os.GetOrders();
                List<OrderDetail> orderDetails = ods.GetOrderDetails();
                decimal totalSales = 0;
                int totalOrderCount = 0;
                double avgOrderSales = 0;

                if (orders != null && orderDetails != null)
                {
                    totalSales = (from o in orders
                                          where o.OrderDate >= startDate && o.OrderDate <= endDate
                                          join od in orderDetails on o.OrderId equals od.OrderId
                                          select od.UnitPrice * od.Quantity * (1 - (decimal)(od.Discount / 100f))).Sum();

                    totalOrderCount = orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).Count();

                    avgOrderSales = (double)totalSales / totalOrderCount;
                } else
                {
                    totalSales = (from o in orders
                                  join od in orderDetails on o.OrderId equals od.OrderId
                                  select od.UnitPrice * od.Quantity * (1 - (decimal)(od.Discount / 100f))).Sum();

                    totalOrderCount = orders.Count();

                    avgOrderSales = (double)totalSales / totalOrderCount;
                }

                lvSaleReport.ItemsSource = new[]
                {
                    new
                    {
                        Period = $"{startDate:dd-mm-yyyy}- {endDate: dd-mm-yyyy}",
                        TotalSales = totalSales,
                        AverageOrder = avgOrderSales,
                        OrderCount = totalOrderCount
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurs while calculating Sales Report {ex.Message}", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
            }
            
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            CalculateSales(dpStartDate.SelectedDate, dpEndDate.SelectedDate);
        }
    }
}
