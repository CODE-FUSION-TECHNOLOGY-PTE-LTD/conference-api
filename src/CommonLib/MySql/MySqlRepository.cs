
using System.Collections.ObjectModel;
using CommonLib.Models;
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

    public async Task<T> GetByEmailAsync(string email)
    {

        var user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        return user as T ?? throw new NullReferenceException($"Entity with email {email} not found");

    }

    public async Task<T> LoginAsync(string email, string password)
    {
        var user = await dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        return user as T ?? throw new NullReferenceException($"Entity with email {email} not found");

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