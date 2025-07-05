using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeDAO
    {
        LucySalesDataContext dbContext = new LucySalesDataContext();

        public Employee? Login (string username, string password)
        {
            return dbContext.Employees.FirstOrDefault(e => e.UserName == username && e.Password == password);
        }

        public List<Employee> GetEmployees()
        {
            return dbContext.Employees.ToList();
        }
    }
}
