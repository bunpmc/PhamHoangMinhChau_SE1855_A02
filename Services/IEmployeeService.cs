using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Services
{
    public interface IEmployeeService
    {
        public Employee? Login(string username, string password);
        public List<Employee> GetEmployees();
    }
}
