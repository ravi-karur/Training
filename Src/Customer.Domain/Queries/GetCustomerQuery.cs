﻿using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApi.Domain.Queries
{
    public class GetCustomerQuery : QueryBase<CustomerDto>
    {
        public GetCustomerQuery()
        {

        }

        [JsonConstructor]
        public GetCustomerQuery(string email)
        {
            Email = email;
        }

        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }
    }
}
