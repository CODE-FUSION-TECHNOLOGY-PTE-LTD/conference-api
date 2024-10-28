
using System.Collections.ObjectModel;
using CommonLib.Models;
using Microsoft.EntityFrameworkCore;
namespace CommonLib.MySql;

public class MySqlRepository<T> : IRepositorySql<T> where T : class, IEntity
{
    private readonly IdentityDbContext identityDbContext;

    public MySqlRepository(IdentityDbContext identityDbContext) => this.identityDbContext = identityDbContext;




    public async Task CreateAsync(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        await identityDbContext.Set<T>().AddAsync(value);
        await identityDbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        var list = await identityDbContext.Set<T>().ToListAsync();
        return new ReadOnlyCollection<T>(list);
    }

    public async Task<T> GetAsync(uint id)
    {
        return await identityDbContext.Set<T>().FindAsync(id) ?? throw new NullReferenceException($"Entity with id {id} not found");
    }

    public async Task<T> GetByEmailAsync(string email)
    {

        var user = await identityDbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        return user as T ?? throw new NullReferenceException($"Entity with email {email} not found");

    }

    public async Task<T> LoginAsync(string email, string password)
    {
        var user = await identityDbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        return user as T ?? throw new NullReferenceException($"Entity with email {email} not found");

    }


    public async Task RemoveAsync(uint id)
    {
        var entity = await GetAsync(id);
        if (entity != null)
        {
            identityDbContext.Remove(entity);
            await identityDbContext.SaveChangesAsync();
        }
    }

    


    public async Task UpdateAsync(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        identityDbContext.Update(value);
        await identityDbContext.SaveChangesAsync();
    }

   

}
