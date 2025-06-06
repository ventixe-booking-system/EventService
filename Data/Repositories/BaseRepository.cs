using Data.Contexts;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;
    protected BaseRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public virtual async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.ToListAsync();
            await _context.SaveChangesAsync();
            return new RepositoryResult<IEnumerable<TEntity>> { Success = true, StatusCode = 200, Result = entities };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.FirstOrDefaultAsync(expression) ?? throw new Exception("Not Found.");
            return new RepositoryResult<TEntity?> { Success = true, StatusCode = 200, Result = entity };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity?> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<bool>> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        var result = await _table.AnyAsync(expression);

        return !result
            ? new RepositoryResult<bool> { Success = false, StatusCode = 404, Error = "Not found." }
            : new RepositoryResult<bool> { Success = true, StatusCode = 200 };
    }

    public virtual async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }
}
