using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Repository;
public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
