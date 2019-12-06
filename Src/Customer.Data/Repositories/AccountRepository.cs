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

namespace CustomerApi.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private const int CREDITLIMIT = 1000;

        private readonly CustomerDbContext _customerDbContext = null;

        public AccountRepository(IOptions<Settings> settings)
        {
            _customerDbContext = new CustomerDbContext(settings);
        }

        public Account GetAccountByCustomerId(Guid customerId)
        {
            return _customerDbContext.Customers.FindSync<Customer>(a => a. == customerId);
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
