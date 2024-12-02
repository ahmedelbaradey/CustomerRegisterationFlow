using System.Linq.Expressions;


namespace CustomerRegisterationFlow.Application.Contracts.Presistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<T> FindAsync(int id);
        Task<IReadOnlyList<T>> FindAllAsync(bool trackChanges);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<IReadOnlyList<T>> FindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
    }
}
