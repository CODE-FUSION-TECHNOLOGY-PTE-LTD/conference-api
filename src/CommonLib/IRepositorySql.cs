
namespace CommonLib;

public interface IRepositorySql<T> where T : class, IEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task CreateAsync(T value);
    Task<T> GetAsync(uint id);
    Task RemoveAsync(uint id);
    Task UpdateAsync(T value);
    Task<T> GetByEmailAsync(string email);
    Task<T> LoginAsync(string email, string pwd);

}


