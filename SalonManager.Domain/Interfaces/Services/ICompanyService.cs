using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface ICompanyService
{
    Task<List<Company>> GetAllAsync();
    Task<Company> GetByIdAsync(int id);
    Task<Company> GetByLoginAsync(string login);
    Task<Company> InsertAsync(InputCompanyModel inputModel);
    Task<Company> UpdateAsync(int id, EditCompanyModel editModel);
    Task<bool> DeleteAsync(int id);

}
