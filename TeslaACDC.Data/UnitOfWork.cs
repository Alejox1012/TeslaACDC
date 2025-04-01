using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data;
using TeslaACDC.Data.Models;
namespace TeslaACDC.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly NikolaContext _context;
    private IRepository<int, Artist> _artistRepository;
    private IRepository<int, Album> _albumRepository;
    private IRepository<int, Track> _trackRepository;
    private bool _disposed = false; 

    public UnitOfWork(NikolaContext context)
    {
        _context = context;
    }

    public IRepository<int, Artist> ArtistRepository
    {
        get
        {
            _artistRepository ??= new Repository<int, Artist>(_context);
            return _artistRepository;
        }
    }

    public IRepository<int, Album> AlbumRepository
    {
        get
        {
            _albumRepository ??= new Repository<int, Album>(_context);
            return _albumRepository;
        }
    }
    public IRepository<int, Track> TrackRepository
    {
        get
        {
            _trackRepository ??= new Repository<int, Track>(_context);
            return _trackRepository;
        }
    }
    public async Task SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException ex)
        {
            ex.Entries.Single().Reload();
        }
        
    }

    #region IDisposable 
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed) 
        {
            if (disposing)
            {
                _context?.Dispose(); 
            }
        }
         _disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
    }
    #endregion
}