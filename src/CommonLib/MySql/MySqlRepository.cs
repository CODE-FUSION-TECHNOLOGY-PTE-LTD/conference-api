
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
namespace CommonLib.MySql;

public class MySqlRepository<T> : IRepositorySql<T> where T : class, IEntity
{
    private readonly MySqlDbContext dbContext;

    public MySqlRepository(MySqlDbContext dbContext) => this.dbContext = dbContext;

    public async Task CreateAsync(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        await dbContext.Set<T>().AddAsync(value);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        var list = await dbContext.Set<T>().ToListAsync();
        return new ReadOnlyCollection<T>(list);
    }

    public async Task<T> GetAsync(uint id)
    {
        return await dbContext.Set<T>().FindAsync(id) ?? throw new NullReferenceException($"Entity with id {id} not found");
    }
    public async Task RemoveAsync(uint id)
    {
        var entity = await GetAsync(id);
        if (entity != null)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        dbContext.Update(value);
        await dbContext.SaveChangesAsync();
    }

}
