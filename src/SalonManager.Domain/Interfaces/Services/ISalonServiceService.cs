using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface ISalonServiceService
{
    Task<List<SalonService>> GetAllAsync(string tenantId);
    Task<SalonService> GetByIdAsync(int id, string tenantId);
    Task<SalonService> InsertAsync(InputSalonServiceModel inputModel);
    Task<SalonService> UpdateAsync(int id, EditSalonServiceModel editModel);
    Task<bool> DeleteAsync(int id);
}
