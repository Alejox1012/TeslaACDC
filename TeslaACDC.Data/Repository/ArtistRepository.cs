using TeslaACDC.Data.Models;
using TeslaACDC.Data.IRepository;
using Microsoft.EntityFrameworkCore;

namespace TeslaACDC.Data.Repository;

public class ArtistRepository<TId, TEntity> : IArtistRepository<TId, TEntity>
where TId : struct
where TEntity : BaseEntity<TId>
{
    private readonly NikolaContext _context;
    internal DbSet<TEntity> _dbset;

    public ArtistRepository(NikolaContext context)
    {
        _context = context;
        _dbset = context.Set<TEntity>();
    }

    public async Task addAsync(TEntity artista)
    {
        
        await _dbset.AddAsync(artista);
        await _context.SaveChangesAsync();

    }

    public async Task<TEntity> FindAsync(TId id)
    {
        return await _dbset.FindAsync(id);
    }
}
