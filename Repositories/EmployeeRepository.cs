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

        public bool AddEmployee(Employee employee)
        {
            return ed.AddEmployee(employee);
        }

        public bool UpdateEmployee(Employee employee)
        {
            return ed.UpdateEmployee(employee);
        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = ed.GetEmployees().FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employee != null)
                {
                    ed.DeleteEmployee(employeeId);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                return false;
            }
        }
    }
}
