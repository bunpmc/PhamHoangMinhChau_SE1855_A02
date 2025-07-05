using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public interface ICustomerRepository
    {
        public List<Customer> GetCustomers();
        public Customer? SearchCustomerById(int customerId);
        public bool AddCustomer(Customer newCustomer);
        public bool DeleteCustomer(int customerId);
        public bool UpdateCustomer(Customer updatedCustomer);
    }
}
