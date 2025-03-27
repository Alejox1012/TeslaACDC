using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.IRepository;

    public interface IArtistRepository<Tid,TEntity>
    where Tid : struct
    where TEntity : BaseEntity<Tid>
    {
    Task addAsync(TEntity artista);
    Task<TEntity> FindAsync(Tid id);
    }

