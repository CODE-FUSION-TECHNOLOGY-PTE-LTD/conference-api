
using System.Linq.Expressions;


namespace CommonLib
{
    public interface IRepository<P> where P : IEntity
    {
        Task CreateAsync(P data);
        Task<IReadOnlyCollection<P>> GetAllAsync();
        Task<IReadOnlyCollection<P>> GetAllAsync(Expression<Func<P, bool>> filter);
        Task<P> GetAsync(uint id);
        Task<P> GetAsync(Expression<Func<P, bool>> filter);
        Task RemoveAsync(uint id);
        Task UpdateAsync(P data);

         Task<IEnumerable<P>> FindAsync(Expression<Func<P, bool>> predicate);
    }
}