using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;

namespace DeveloperTest.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public CustomerModel[] GetCustomers()
        {
            return context.Customers.Select(customer => new CustomerModel
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Type = customer.Type
            }).ToArray();
        }

        public CustomerModel GetCustomer(int customerId)
        {
            return context.Customers.Where(customer => customer.CustomerId == customerId).Select(customer => new CustomerModel
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Type = customer.Type
            }).SingleOrDefault();
        }

        public CustomerModel CreateCustomer(BaseCustomerModel model)
        {
            var createdCustomer = context.Customers.Add(new Customer
            {
                Name = model.Name,
                Type = model.Type
            });

            context.SaveChanges();

            return new CustomerModel
            {
                CustomerId = createdCustomer.Entity.CustomerId,
                Name = createdCustomer.Entity.Name,
                Type = createdCustomer.Entity.Type
            };
        }
    }
}
