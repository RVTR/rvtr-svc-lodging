using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.Domain.Abstracts;
using RVTR.Lodging.Domain.Interfaces;

namespace RVTR.Lodging.Context.Repositories
{
  /// <summary>
  /// Represents the _Repository_ class
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : AEntity
  {
    public readonly DbSet<TEntity> _dbSet;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public Repository(LodgingContext context)
    {
      _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(int id) => _dbSet.Remove((await SelectAsync(e => e.EntityId == id)).FirstOrDefault());

    /// <summary>
    ///
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public virtual async Task InsertAsync(TEntity entry) => await _dbSet.AddAsync(entry).ConfigureAwait(true);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> SelectAsync()
    {
      return await LoadAsync(await _dbSet.ToListAsync());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate)
    {
      return await LoadAsync(await _dbSet.Where(predicate).ToListAsync().ConfigureAwait(true));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="entry"></param>
    public virtual void Update(TEntity entry) => _dbSet.Update(entry);

    /// <summary>
    ///
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    private async Task<IEnumerable<TEntity>> LoadAsync(IEnumerable<TEntity> entities)
    {
      foreach (var entity in entities)
      {
        foreach (var navigation in _dbSet.Attach(entity).Navigations)
        {
          await navigation.LoadAsync();
        }
      }

      return entities;
    }
  }
}
