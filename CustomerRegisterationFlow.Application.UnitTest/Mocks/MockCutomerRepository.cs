using Moq;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Domain.Entities;

namespace CustomerRegisterationFlow.IntegrationTest.Mocks
{
    public static class MockCutomerRepository
    {
        public static Mock<ICustomerRepository> GetCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    ICNumber = 3894444,
                    Name = "Ahmed",
                    Email ="email@email.com",
                    Phone ="0110"
                },
                new Customer
                {
                    Id = 2,
                    ICNumber = 13895555,
                    Name = "Ahmed1",
                    Email ="email1@email.com",
                    Phone ="01101"
                },
            };

            var mockRepo = new Mock<ICustomerRepository>();

            mockRepo.Setup(c => c.FindAllAsync(false)).ReturnsAsync(customers);
            mockRepo.Setup(c => c.FindByConditionAsync(c=>c.ICNumber.Equals(3894444),false)).ReturnsAsync(customers.First());

            mockRepo.Setup(r => r.Create(It.IsAny<Customer>())).ReturnsAsync((Customer customer) => 
            {
                customers.Add(customer);
                return customer;
            });
            return mockRepo;

        }
    }
}
