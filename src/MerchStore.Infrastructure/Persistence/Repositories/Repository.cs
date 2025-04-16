using Microsoft.EntityFrameworkCore;
using MerchStore.Domain.Common;
using MerchStore.Domain.Interfaces;

namespace MerchStore.Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    protected readonly AppDbContext _context;

    protected readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    // Retrieves an entity by its ID
    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    // Retrieves all entities of this type
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    // Adds a new entity to the database
    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    // Updates an existing entity in the database
    public virtual Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    // Removes an entity from the database
    public virtual Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
}