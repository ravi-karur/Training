using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await ModelDbSets.AsNoTracking().AnyAsync(e => e.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));

        }
    }
}
