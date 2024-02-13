using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface ICompanyService
{
    Task<List<Company>> GetAllAsync(string tenantId = "");
    Task<Company> GetByIdAsync(int id, string tenantId = "");
    Task<Company> InsertAsync(InputCompanyModel inputModel);
    Task<Company> UpdateAsync(int id, EditCompanyModel editModel);
    Task<bool> DeleteAsync(int id);

}
