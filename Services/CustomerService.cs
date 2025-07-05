using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Repositories;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;
        public List<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }
        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }
        public bool AddCustomer(Customer newCustomer)
        {
            return _customerRepository.AddCustomer(newCustomer);
        }

        public bool DeleteCustomer(int customerId)
        {
            return _customerRepository.DeleteCustomer(customerId);
        }

        public Customer? SearchCustomerById(int customerId)
        {
            return _customerRepository.SearchCustomerById(customerId);
        }

        public bool UpdateCustomer(Customer updatedCustomer)
        {
            return _customerRepository.UpdateCustomer(updatedCustomer);
        }
    }
}
