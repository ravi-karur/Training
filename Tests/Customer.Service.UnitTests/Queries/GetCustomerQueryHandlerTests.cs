using AutoMapper;
using CustomerApi.Data.Interfaces;
using CustomerApi.Data.Persistence;
using CustomerApi.Domain.Common.Exceptions;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Models;
using CustomerApi.Domain.Queries;
using CustomerApi.Service.Handlers.Query;
using CustomerApi.Service.UnitTests.Common;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Service.UnitTests.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomerQueryHandlerTests
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCustomerQueryHandler> _logger;
        private readonly ICustomerRepository _customerRepository;


        public GetCustomerQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _logger = Mock.Of<ILogger<GetCustomerQueryHandler>>();
        }

        [Fact]
        public async Task GetCustomerDetailExists()
        {
            var email = "test@test.com.au";
            var mock = new Mock<ICustomerRepository>();
            mock.Setup(srv => srv.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(new Customer("Test1", email, 0, 0));

            var sut = new GetCustomerQueryHandler(_logger, _mapper, mock.Object);

            var result = await sut.Handle(new GetCustomerQuery { Email = email }, CancellationToken.None);

            Assert.IsType<CustomerDto>(result);   
            
            Assert.Equal("Test1", result.Name);
            Assert.Equal(email, result.Email);

            mock.Verify(rep => rep.GetCustomerByEmail(email), Times.Once);
        }

        [Fact]
        public async Task GetCustomerDetailNotFound()
        {
            var sut = new GetCustomerQueryHandler(_logger, _mapper, null);

            var exceptionResult = await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new GetCustomerQuery { Email = "test@test.com.au" }, CancellationToken.None));

            Assert.Contains("not found", exceptionResult.Message);

        }
    }
}
