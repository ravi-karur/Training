using CustomerApi.Domain.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CustomerApi.Domain.Commands
{
    public class CreateCustomerCommand : CommandBase<CustomerDto>
    {
        public CreateCustomerCommand()
        {
        }

        [JsonConstructor]
        public CreateCustomerCommand(string name, string email, uint monthlyIncome, uint monthlyExpense)
        {
            Name = name;
            Email = email;
            MonthlyIncome = monthlyIncome;
            MonthlyExpense = monthlyExpense;
        }

        [JsonProperty("name")]        
        [MaxLength(50)]
        public string Name { get; }

        [JsonProperty("email")]        
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; }
       
        [JsonProperty("monthlyIncome")]        
        public uint MonthlyIncome { get; }

        [JsonProperty("monthlyExpense")]        
        public uint MonthlyExpense { get; }
    }
}
