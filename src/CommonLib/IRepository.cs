
using System.Linq.Expressions;
using CommonLib;

namespace common.Api
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
    }
}