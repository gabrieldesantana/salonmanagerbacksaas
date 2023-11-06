using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync(string tenantId);
    Task<Customer> GetByIdAsync(int id, string tenantId);
    Task<Customer> InsertAsync(InputCustomerModel inputModel);
    Task<Customer> UpdateAsync(int id, EditCustomerModel editModel);
    Task<bool> DeleteAsync(int id);
}
