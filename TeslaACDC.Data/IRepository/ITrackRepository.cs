using TeslaACDC.Data.Models;

namespace TeslaACDC.Data.IRepository;

    public interface ITrackRepository<Tid,TEntity>
    where Tid : struct
    where TEntity : BaseEntity<Tid>
    {
    Task addAsync(TEntity track);
    Task<TEntity> FindAsync(Tid id);
    }

