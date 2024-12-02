using System;
using System.Threading.Tasks;

namespace CustomerRegisterationFlow.Application.Contracts.Presistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        Task SaveAsync();
    }
}
