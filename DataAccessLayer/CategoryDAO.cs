using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        LucySalesDataContext context = new LucySalesDataContext();

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }
    }
}
