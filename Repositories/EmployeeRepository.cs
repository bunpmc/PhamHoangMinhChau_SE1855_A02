using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeDAO ed = new EmployeeDAO();
        public Employee? Login(string username, string password)
        {
            return ed.Login(username, password);
        }

        public List<Employee> GetEmployees()
        {
            return ed.GetEmployees();
        }
    }
}
