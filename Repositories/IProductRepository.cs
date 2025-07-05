using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();
        public List<Product> GetProductByCategoryId(int categoryId);
        public bool AddProduct(Product product);
        public bool DeleteProduct(int productId);
        public bool UpdateProduct(Product product);
    }
}
