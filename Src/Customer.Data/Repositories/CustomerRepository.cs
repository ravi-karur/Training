using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CustomerApi.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private const int CREDITLIMIT = 1000;

        private readonly CustomerDbContext _customerDbContext = null;

        public CustomerRepository(IOptions<Settings> settings)
        {
            _customerDbContext = new CustomerDbContext(settings);
        }

        public Customer GetCustomerByEmail(string email)
        {
            var customerDetail = _customerDbContext.Customers.FindSync<Customer>(customer => customer.Email == email).Current
            //var customerDetail = ModelDbSets.AsNoTracking().Where(e => e.Email.Equals(email)).FirstOrDefault();

            if ( customerDetail != null)
            {
                return customerDetail;
            }

            throw new NotFoundException($"Customer with email {email} was not found");
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await ModelDbSets.AsNoTracking().AnyAsync(e => e.Email.Equals(email));
        }

        public Account GetAccountByCustomerId(Guid customerId)
        {
            return _customerDbContext.Customers.FindSync(a => a.CustomerId == customerId).FirstOrDefault();
        }

        public Account GetAccountByEmail(string email)
        {
            return _customerDbContext.Accounts.Where(a => a.Email == email).FirstOrDefault();
        }

        public bool IsCustomerEligibleForAccount(Customer customer)
        {

            return (customer.MonthlyIncome - customer.MonthlyExpense) >= CREDITLIMIT ? true : false;
        }
    }
}
