using CustomerRegisterationFlow.Application.Contracts.Presistence;
using CustomerRegisterationFlow.Domain.Entities;

namespace CustomerRegisterationFlow.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(RepositoryContext dbContext) : base(dbContext)
        {
       
        }
        public async Task<bool> EmailExists(string email)
        {
            var entity = await FindByConditionAsync(c => c.Email.Equals(email), trackChanges: false);
            return entity != null;
        }
        public async Task<bool> ICNumberExists(int icNumber)
        {
            var entity = await FindByConditionAsync(c => c.ICNumber.Equals(icNumber), trackChanges: false);
            return entity != null;
        }
        public async Task<bool> PhoneExists(string phone)
        {
            var entity = await FindByConditionAsync(c => c.Phone.Equals(phone), trackChanges: false);
            return entity != null;
        }
    }
}
 