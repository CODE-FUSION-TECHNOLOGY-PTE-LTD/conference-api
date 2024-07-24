
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommonLib.MySql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where TContext : DbContext
    {
        services.AddDbContext<TContext>(optionsAction);
        return services;
    }

    public static IServiceCollection AddMySqlRepository<TEntity, TContext>(this IServiceCollection services)
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        services.AddScoped<IRepositorySql<TEntity>, MySqlRepository<TEntity>>();
        return services;
    }
}