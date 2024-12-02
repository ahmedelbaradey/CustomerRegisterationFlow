using CustomerRegisterationFlow.Domain.Entities;


namespace CustomerRegisterationFlow.Application.Contracts.Presistence
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<bool> EmailExists(string email);
        Task<bool> ICNumberExists(int icNumber);
        Task<bool> PhoneExists(string phone);
    }
}
