using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        CustomerDAO cd =  new CustomerDAO();
        public List<Customer> GetCustomers()
        {
            return cd.GetCustomers();
        }
        public bool AddCustomer(Customer newCustomer)
        {
            return cd.AddCustomer(newCustomer);
        }

        public bool DeleteCustomer(int customerId)
        {
            return cd.DeleteCustomer(customerId);
        }

        public Customer? SearchCustomerById(int customerId)
        {
            return cd.SearchCustomerById(customerId);
        }

        public bool UpdateCustomer(Customer updatedCustomer)
        {
            return cd.UpdateCustomer(updatedCustomer);
        }
    }
}
