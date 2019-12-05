using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Domain.Commands;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Service.Handlers.Command
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly ICustomerDbContext _customerDbContext;
        private readonly IMapper _customerMap;
        private readonly ILogger _logger;

        public CreateAccountCommandHandler(ILogger<CreateAccountCommandHandler> logger, IMapper customerMap, ICustomerDbContext customerDbContext)
        {
            _logger = logger;
            _customerMap = customerMap;
            _customerDbContext = customerDbContext ?? throw new ArgumentNullException(nameof(customerDbContext));
        }
        public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerDetail = await _customerDbContext.Customers.FindAsync(request.Email);

                if (customerDetail != null)
                {
                    if (isEligibleForZipPayCredit(customerDetail.MonthlyIncome, customerDetail.MonthlyExpense))
                    {
                        var accountInfo = await _customerDbContext.Accounts.FindAsync(request.Email);

                        if (accountInfo is null)
                        {
                            Account newAccount = new Account(request.Email, customerDetail.Id);
                            newAccount.Active = true;


                            _customerDbContext.Accounts.Add(newAccount);
                            await _customerDbContext.SaveChangesAsync(cancellationToken);


                            var customerDto = _customerMap.Map<AccountDto>(newAccount);

                            return customerDto;
                        }
                        else
                        {
                            throw new BadRequestException("Customer already has an account");
                        }
                    }
                    else
                    {
                        throw new BadRequestException("Customer not eligible for ZipPay credit");
                    }

                }
                else
                {
                    throw new NotFoundException("Customer does not exist and need to register before creating an account");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            
            
        }

        
        private bool isEligibleForZipPayCredit(uint monthlyIncome, uint monthlyExpense)
        {
            return (monthlyIncome - monthlyExpense) >= 1000 ? true : false;
        }
    }
}
