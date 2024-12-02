using CustomerRegisterationFlow.Application.Contracts.Presistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace CustomerRegisterationFlow.Persistence.Repositories
{
    public  class GenericRepository<T> : IGenericRepository<T> where T : class
    {
 
 
        private readonly RepositoryContext RepositoryContext;
 

        public GenericRepository(RepositoryContext dbContext)
        {
            RepositoryContext = dbContext;
        }
        public async Task<T> Create(T entity)
        {
            await RepositoryContext.AddAsync(entity);
            return entity;
        }
        public async Task Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public async Task Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
        public async Task AddRangeAsync(List<T> entities) => await RepositoryContext.Set<T>().AddRangeAsync(entities);
        public async Task<IReadOnlyList<T>> FindAllAsync(bool trackChanges) => !trackChanges ? await RepositoryContext.Set<T>().AsNoTracking().ToListAsync() : await RepositoryContext.Set<T>().ToListAsync();
        public async Task<IReadOnlyList<T>> FindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? await RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync() : await RepositoryContext.Set<T>().Where(expression).ToListAsync();
        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? await RepositoryContext.Set<T>().Where(expression).AsNoTracking().SingleOrDefaultAsync() : await RepositoryContext.Set<T>().Where(expression).SingleOrDefaultAsync();
        public async Task<T> FindAsync(int id) => await RepositoryContext.Set<T>().FindAsync(id);

    }
}
