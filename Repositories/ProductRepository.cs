using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        ProductDAO pd = new ProductDAO();
        public bool AddProduct(Product product)
        {
            return pd.AddProduct(product);
        }

        public bool DeleteProduct(int productId)
        {
            return pd.DeleteProduct(productId);
        }

        public List<Product> GetAllProducts()
        {
            return pd.GetAllProducts();
        }

        public List<Product> GetProductByCategoryId(int categoryId)
        {
            return pd.GetProductByCategoryId(categoryId);
        }

        public bool UpdateProduct(Product product)
        {
            return pd.UpdateProduct(product);
        }
    }
}
