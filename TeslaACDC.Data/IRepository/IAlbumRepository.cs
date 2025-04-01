using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.IRepository;

    public interface IAlbumRepository<Tid,TEntity>
    where Tid : struct
    where TEntity : BaseEntity<Tid>
    {
    Task addAsync(TEntity album);
    Task<TEntity> FindAsync(Tid id);
    
    }

