using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        CategoryDAO cd = new CategoryDAO();
        public List<Category> GetCategories()
        {
            return cd.GetCategories();
        }
    }
}
