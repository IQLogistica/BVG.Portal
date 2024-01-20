using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public abstract class PostgreSQLRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _set;

    public PostgreSQLRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _set = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _set.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _set.FindAsync(id);
    }

    public void Add(TEntity entity)
    {
        _set.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _set.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        _set.Remove(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _set.AddRange(entities);
    }
}