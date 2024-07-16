using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SuperAparcamiento.DataAccess.Persistence;
using SuperAparcamiento.DataAccess.Repositories.Interfaces;
using SuperAparcamiento.Model.Common;
using SuperAparcamiento.Model.Exceptions;
using System.Linq.Expressions;

namespace SuperAparcamiento.DataAccess.Repositories;

/// <summary>
/// Repositorio base para la gestión de entidades (genérico)
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class BaseRepository<TEntity>(AparcamientoDbContext Context) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> DbSet = Context.Set<TEntity>();

    /// <summary>
    /// Inserta una entidad en la base de datos
    /// </summary>
    /// <param name="entity">Entidad a insertar</param>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = (await DbSet.AddAsync(entity)).Entity;
        await Context.SaveChangesAsync();

        return addedEntity;
    }

    /// <summary>
    /// Elimina una entidad de la base de datos
    /// </summary>
    /// <param name="entity">Entidad a eliminar</param>
    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var removedEntity = DbSet.Remove(entity).Entity;
        await Context.SaveChangesAsync();

        return removedEntity;
    }

    /// <summary>
    /// Obtiene todas las entidades que cumplan con un predicado
    /// </summary>
    /// <param name="predicate">Filtro de búsqueda</param>
    public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.Where(predicate).ExecuteDeleteAsync();
    }

    /// <summary>
    /// Obtiene todas las entidades que cumplan con un predicado
    /// </summary>
    /// <param name="predicate">Filtro de búsqueda</param>
    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = DbSet.AsQueryable();

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Obtiene la primera entidad que cumpla con un predicado
    /// </summary>
    /// <param name="predicate"></param>
    /// <exception cref="ResourceNotFoundException"> No se encuentra el recurso </exception>
    public virtual async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = DbSet.AsQueryable();

        if (include != null)
        {
            query = include(query);
        } 

        return await query.Where(predicate).FirstOrDefaultAsync(); 
    }

    /// <summary>
    /// Actualiza una entidad en la base de datos
    /// </summary>
    /// <param name="entity">Registro a actualizar</param>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }
}

