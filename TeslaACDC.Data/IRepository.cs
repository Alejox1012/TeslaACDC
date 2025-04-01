// Created by: TeslaACDC
// Created on: 2023-10-01
using System.Linq.Expressions;
using TeslaACDC.Data.Models;

public interface IRepository<Tid, TEntity>
    where Tid : struct
    where TEntity : BaseEntity<Tid>
    {
       Task AddAsync(TEntity entity);
       Task<TEntity> FindAsync(Tid id);
       Task updateAsync(TEntity entity);
       Task deleteAsync(TEntity entity);
       Task deleteAsync(Tid id);
       Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = ""
       );


    }
