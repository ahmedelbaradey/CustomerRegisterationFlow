
using AutoMapper;
using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using CustomerRegisterationFlow.Application.Contracts.Presistence;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomerRegisterationFlow.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        public ICustomerRepository CustomerRepository => _customerRepository.Value;
        public UnitOfWork(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(repositoryContext));
        }
        public void Dispose()
        {
            _repositoryContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
