﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.Dtos
{
    public class CustomerDto
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("monthlyIncome")]
        public uint MonthlyIncome { get; set; }

        [JsonProperty("monthlyExpense")]
        public uint MonthlyExpense { get; set; }

    }
}
