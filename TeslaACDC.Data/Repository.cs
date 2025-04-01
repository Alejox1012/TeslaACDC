using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.Models;

public class Repository<Tid, TEntity> : IRepository<Tid, TEntity>
    where Tid : struct
    where TEntity : BaseEntity<Tid>
{
    internal NikolaContext _context;
    internal DbSet<TEntity> _dbSet;

    public Repository(NikolaContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);    
    }

    public virtual async Task deleteAsync(TEntity entity)
    {
        if(_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public virtual async Task deleteAsync(Tid id)
    {
        TEntity entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete == null)
        {
            throw new ArgumentNullException($"Entity with id {id} not found.");
        }
        await deleteAsync(entityToDelete);
        
    }

    public virtual async Task<TEntity> FindAsync(Tid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if(filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(
            new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task updateAsync(TEntity entity)
    {
       _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}
// Compare this snippet from TeslaACDC.Data/Models/Album.cs: