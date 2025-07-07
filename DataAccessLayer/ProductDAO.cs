using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        LucySalesDataContext context = new LucySalesDataContext();

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public List<Product> GetProductByCategoryId(int categoryId)
        {
            return context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        public Product? GetProductById(int productId)
        {
            return context.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public bool AddProduct(Product product)
        {
            try
            {
                context.Products.Add(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(int productId) {
            try
            {
                var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.SupplierId = product.SupplierId;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.QuantityPerUnit = product.QuantityPerUnit;
                    existingProduct.UnitPrice = product.UnitPrice;
                    existingProduct.UnitsInStock = product.UnitsInStock;
                    existingProduct.UnitsOnOrder = product.UnitsOnOrder;
                    existingProduct.ReorderLevel = product.ReorderLevel;
                    existingProduct.Discontinued = product.Discontinued;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
