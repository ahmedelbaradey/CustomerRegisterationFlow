using AutoMapper;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Application.DTOs.Customers;
using CustomerRegisterationFlow.Application.Features.Customers.Commands.AddCustomerBasicInfo;
using CustomerRegisterationFlow.Application.Profiles;
using CustomerRegisterationFlow.Application.Responses;
using CustomerRegisterationFlow.Infrastructure.OTP;
using CustomerRegisterationFlow.IntegrationTest.Mocks;
using CustomerRegisterationFlow.LoggerService;
using CustomerRegisterationFlow.Resources;
using Microsoft.Extensions.Localization;
using Moq;
using Shouldly;
using Xunit;


namespace CustomerRegisterationFlow.IntegrationTest.Customers.Commands.AddCustomerBasicInfo
{
    public class AddCustomerBasicInfoCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<TOTP> _iTOTP;
        private readonly Mock<IStringLocalizer<SharedResources>> _localizer;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<LoggerManager> _mocklogger;
        private readonly CustomerBasicInfoDto _customerBasicInfoDtoDto;
        private readonly AddCustomerBasicInfoCommandHandler _handler;
        public AddCustomerBasicInfoCommandHandlerTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

              var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _iTOTP = new Mock<TOTP>();
            _localizer = new Mock<IStringLocalizer<SharedResources>>();
            _mocklogger = new Mock<LoggerManager>();
             _mapper = mapperConfig.CreateMapper();
            _handler = new AddCustomerBasicInfoCommandHandler(_mockUow.Object, _mapper,_iTOTP.Object, _mocklogger.Object, _localizer.Object);
            _customerBasicInfoDtoDto = new CustomerBasicInfoDto
            {
                    ICNumber = 13894444,
                    Name = "Ahmed1",
                    Email ="email1@email.com",
                    Phone ="01101"
            };
        }
        [Fact]
        public async Task Valid_Customer_Added()
        {
            var result = await _handler.Handle(new AddCustomerBasicInfoCommand() { CustomerBasicInfoDto = _customerBasicInfoDtoDto }, CancellationToken.None);

            var customers = await _mockUow.Object.CustomerRepository.FindAllAsync(trackChanges: false);

            result.ShouldBeOfType<BaseCommandResponse>();

            customers.Count.ShouldBe(3);
        }

    }
}
