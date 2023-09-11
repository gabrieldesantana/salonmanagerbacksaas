using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface IFinanceService
{
    Task<List<Finance>> GetAllAsync();
    Task<Finance> GetByIdAsync(int id);
    Task<Finance> InsertAsync(InputFinanceModel inputModel);
    Task<Finance> UpdateAsync(int id, EditFinanceModel editModel);
    Task<bool> DeleteAsync(int id);
}
