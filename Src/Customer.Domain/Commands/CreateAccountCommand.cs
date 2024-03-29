﻿using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApi.Domain.Commands
{
    public class CreateAccountCommand : CommandBase<AccountDto>
    {
        [JsonConstructor]
        public CreateAccountCommand()
        {
        }
        
        public CreateAccountCommand(string email)
        {   
            Email = email;
        }


        [JsonProperty("email")]
        [Required]
        [MaxLength(50)]
        [JsonRequired]
        [EmailAddress]
        public string Email { get; set; }

        [JsonProperty("accountNo")]
        public long AccountNo { get; set; }

        [JsonProperty("customerId")]
        public Guid customerId { get; set; }

        

    }
}
