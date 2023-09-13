using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;

namespace SalonManager.Infra.Data.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly SalonManagerDbContext _context;
    protected readonly DbSet<T> _dbSet;
    private readonly IUnitOfWork _unitOfWork;

    public GenericRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _unitOfWork = unitOfWork;
    }

    public virtual async Task <List<T>> GetAllAsync()
    {
        return await _dbSet
            .AsNoTracking().Where(x => x.Actived == true).ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet
        .FirstOrDefaultAsync(x => x.Id == id && x.Actived == true);

        return entity; 
    }

    public async Task<T> InsertAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _unitOfWork.Commit();
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
        }
       
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var result = await GetByIdAsync(entity.Id);

        if (result is not null)
        {
            try
            {
                _context.Entry(result).CurrentValues.SetValues(entity);
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
            }
        }
        return entity;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var response = await GetByIdAsync(id);

            response.Actived = false;

            await _unitOfWork.Commit();
            return true;
        }
        catch (Exception)
        {
            await _unitOfWork.Rollback();
            return false;
        }
    }
}
