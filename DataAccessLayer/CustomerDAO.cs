using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        LucySalesDataContext dbContext = new LucySalesDataContext();
        public List<Customer> GetCustomers()
        {
            return dbContext.Customers.ToList();
        }
        public Customer? Login(string phone)
        {
            return dbContext.Customers.FirstOrDefault(predicate => predicate.Phone == phone);
        }
        public Customer? SearchCustomerById(int customerId)
        {
            return dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public bool AddCustomer(Customer newCustomer)
        {
            try
            {
                dbContext.Customers.Add(newCustomer);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            var customer = dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer != null)
            {
                dbContext.Remove(customer);
                return dbContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool UpdateCustomer(Customer updatedCustomer)
        {
            try
            {
                var existingCustomer = dbContext.Customers.FirstOrDefault(c => c.CustomerId == updatedCustomer.CustomerId);
                if (existingCustomer != null)
                {
                    existingCustomer.CompanyName = updatedCustomer.CompanyName;
                    existingCustomer.ContactName = updatedCustomer.ContactName;
                    existingCustomer.ContactTitle = updatedCustomer.ContactTitle;
                    existingCustomer.Address = updatedCustomer.Address;
                    existingCustomer.Phone = updatedCustomer.Phone;
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    } 
}
