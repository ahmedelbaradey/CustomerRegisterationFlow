using Moq;
using CustomerRegisterationFlow.Application.Contracts.Presistence;

namespace CustomerRegisterationFlow.IntegrationTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockCustomerRepo = MockCutomerRepository.GetCustomerRepository();
            mockUow.Setup(r => r.CustomerRepository).Returns(mockCustomerRepo.Object);
            return mockUow;
        }
    }
}
