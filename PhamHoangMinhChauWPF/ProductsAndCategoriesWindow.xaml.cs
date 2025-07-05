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
    /// Interaction logic for ProductsAndCategoriesWindow.xaml
    /// </summary>
    public partial class ProductsAndCategoriesWindow : Window
    {
        ProductService ps = new ProductService();
        CategoryService cs = new CategoryService();
        public ProductsAndCategoriesWindow()
        {
            InitializeComponent();
            LoadDataIntoTreeViewAndListView();
        }

        private void LoadDataIntoTreeViewAndListView()
        {
            try
            {
                TreeViewItem root = new TreeViewItem();
                root.Header = "Categories";
                tvCategory.Items.Add(root);
                List<Category> categories = cs.GetCategories();
                if (categories != null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        TreeViewItem categoryItem = new TreeViewItem();
                        categoryItem.Header = category.CategoryName;
                        categoryItem.Tag = category;
                        root.Items.Add(categoryItem);

                        List<Product> products = ps.GetProductByCategoryId(category.CategoryId);
                        if (products != null && products.Count > 0)
                        {
                            foreach (var product in products)
                            {
                                TreeViewItem productItem = new TreeViewItem();
                                productItem.Header = product.ProductName;
                                productItem.Tag = product;
                                categoryItem.Items.Add(productItem);
                            }
                        }
                        else
                        {
                            categoryItem.Items.Add(new ListViewItem { Content = "No products available" });
                        }
                    }
                    root.ExpandSubtree();
                }
                else
                {
                    MessageBox.Show("No categories found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtQuantityPerUnit.Clear();
            txtCategoryId.Clear();
            txtReorderLevel.Clear();
            txtSupplierId.Clear();
            txtUnitsOnOrder.Clear();
            chkDiscontinued.IsChecked = false;
        }

        private void tvCategory_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null) return;

            TreeViewItem selectedItem = e.NewValue as TreeViewItem;

            if(selectedItem == null) return;

            object data = selectedItem.Tag;

            if(data == null)
            {
                lvProduct.ItemsSource = ps.GetAllProducts();
            }
            else if (data is Category selectedCategory)
            {
                lvProduct.ItemsSource = ps.GetProductByCategoryId(selectedCategory.CategoryId);

            } else if (data is Product selectedProduct)
            {
                lvProduct.ItemsSource = new List<Product> { selectedProduct };
                
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product
                {
                    ProductId = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                    ProductName = txtName.Text,
                    SupplierId = string.IsNullOrEmpty(txtSupplierId.Text) ? (int?)null : int.Parse(txtSupplierId.Text),
                    CategoryId = string.IsNullOrEmpty(txtCategoryId.Text) ? (int?)null : int.Parse(txtCategoryId.Text),
                    QuantityPerUnit = txtQuantityPerUnit.Text,
                    UnitPrice = string.IsNullOrEmpty(txtPrice.Text) ? (decimal?)null : decimal.Parse(txtPrice.Text),
                    UnitsInStock = string.IsNullOrEmpty(txtQuantity.Text) ? (int?)null : int.Parse(txtQuantity.Text),
                    UnitsOnOrder = string.IsNullOrEmpty(txtUnitsOnOrder.Text) ? (int?)null : int.Parse(txtUnitsOnOrder.Text),
                    ReorderLevel = string.IsNullOrEmpty(txtReorderLevel.Text) ? (int?)null : int.Parse(txtReorderLevel.Text),
                    Discontinued = chkDiscontinued.IsChecked ?? false
                };
                List<Product> existingProducts = ps.GetAllProducts();
                if (!existingProducts.Contains(product))
                {

                    if (ps.AddProduct(product))
                    {
                        MessageBox.Show("Product added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (ps.UpdateProduct(product))
                    {
                        MessageBox.Show("Product updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvProduct.SelectedItem is Product selectedProduct)
                {
                    if (MessageBox.Show($"Are you sure you want to delete the product '{selectedProduct.ProductName}'?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        if (ps.DeleteProduct(selectedProduct.ProductId))
                        {
                            MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadDataIntoTreeViewAndListView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete product.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProduct.SelectedItem is Product selectedProduct)
            {
                txtId.Text = selectedProduct.ProductId.ToString();
                txtName.Text = selectedProduct.ProductName;
                txtSupplierId.Text = selectedProduct.SupplierId?.ToString() ?? string.Empty;
                txtCategoryId.Text = selectedProduct.CategoryId?.ToString() ?? string.Empty;
                txtQuantityPerUnit.Text = selectedProduct.QuantityPerUnit;
                txtPrice.Text = selectedProduct.UnitPrice?.ToString("F2") ?? string.Empty;
                txtQuantity.Text = selectedProduct.UnitsInStock?.ToString() ?? string.Empty;
                txtUnitsOnOrder.Text = selectedProduct.UnitsOnOrder?.ToString() ?? string.Empty;
                txtReorderLevel.Text = selectedProduct.ReorderLevel?.ToString() ?? string.Empty;
                chkDiscontinued.IsChecked = selectedProduct.Discontinued;
            }
            else
            {
                btnDelete.IsEnabled = false;
            }
        }
    }
}
