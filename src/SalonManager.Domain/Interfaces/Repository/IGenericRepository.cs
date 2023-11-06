using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Repository;
public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllByTenantIdAsync(string tenantId);
    Task<T> GetByIdAsync(int id);
    Task<T> GetByIdByTenantIdAsync(int id, string tenantId);
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity, string tenantId);
    Task<bool> DeleteAsync(int id);
}
