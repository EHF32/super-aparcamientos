using Microsoft.EntityFrameworkCore.Query;
using SuperAparcamiento.Model.Common;
using System.Linq.Expressions;

namespace SuperAparcamiento.DataAccess.Repositories.Interfaces;

/// <summary>
/// Interfaz base para los repositorios
/// </summary>
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Obtiene el primer elemento que cumple con el predicado
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    /// <summary>
    /// Obtiene la lista de elementos que cumplen con el predicado
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns> 
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

    /// <summary>
    /// Añade un nuevo elemento
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Actualiza un elemento
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>

    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Elimina un elemento
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<TEntity> DeleteAsync(TEntity entity);

    /// <summary>
    /// Elimina varios elementos que cumplan con el predicado
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
}
