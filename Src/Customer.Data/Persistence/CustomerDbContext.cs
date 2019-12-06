using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Data.Persistence
{
    public class CustomerDbContext
    {
        private readonly IMongoDatabase _database = null;

        public CustomerDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Customer> Customers
        {
            get
            {
                return _database.GetCollection<Customer>("Customer");
            }
        }
    }
}
