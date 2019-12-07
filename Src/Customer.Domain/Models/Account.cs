﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Account : ModelBase
    {   
        public string Email { get; set; }
        public long AccountNo { get; set; }
        public bool Active { get; set; }

        public Account(string email)
        {
            Email = email;
        }

        
    }
}