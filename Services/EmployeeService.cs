using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Repositories;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }
        public Employee? Login(string username, string password)
        {
            return _employeeRepository.Login(username, password);
        }
        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public bool AddEmployee(Employee employee)
        {
            return _employeeRepository.AddEmployee(employee);
        }

        public bool UpdateEmployee(Employee employee)
        {
            return _employeeRepository.UpdateEmployee(employee);
        }
        public bool DeleteEmployee(int employeeId)
        {
            return _employeeRepository.DeleteEmployee(employeeId);

        }
    
}

