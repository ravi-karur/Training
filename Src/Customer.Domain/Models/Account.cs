using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.Models
{
    public class Account : ModelBase
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        public long AccountNo { get; set; }
        public bool Active { get; set; }

        public Account(string email, Guid id)
        {
            Email = email;
            Id = id;
        }

        
    }
}