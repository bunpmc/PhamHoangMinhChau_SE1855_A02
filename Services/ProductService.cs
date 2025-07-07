using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public Product? GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }

        public bool AddProduct(Product product)
        {
            return _productRepository.AddProduct(product);
        }

        public bool DeleteProduct(int productId)
        {
            return _productRepository.DeleteProduct(productId);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public List<Product> GetProductByCategoryId(int categoryId)
        {
            return _productRepository.GetProductByCategoryId(categoryId);
        }

        public bool UpdateProduct(Product product)
        {
            return _productRepository.UpdateProduct(product);
        }
    }
}
