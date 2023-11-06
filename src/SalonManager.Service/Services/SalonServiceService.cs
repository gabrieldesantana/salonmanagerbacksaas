using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services;

public class SalonServiceService : ISalonServiceService
{
    private readonly ISalonServiceRepository _repository;
    public SalonServiceService(ISalonServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SalonService>> GetAllAsync(string tenantId = "")
    {
        List<SalonService>? services;

        if (tenantId == "")
            services = await _repository.GetAllAsync();
        else
            services = await _repository.GetAllByTenantIdAsync(tenantId);

        if (services is null) return new List<SalonService>();

        return services;
    }

    public async Task<SalonService> GetByIdAsync(int id, string? tenantId = "")
    {
        SalonService? service;

        if (tenantId == "")
            service = await _repository.GetByIdAsync(id);
        else
            service = await _repository.GetByIdByTenantIdAsync(id, tenantId);

        if (service is null) return new SalonService();

        return service;
    }

    public async Task<SalonService> InsertAsync(InputSalonServiceModel inputModel)
    {
        var newSalonService = new SalonService
        {
            TenantId = inputModel.TenantId.ToString(),
            Name = inputModel.Name,
            Category = inputModel.Category,
            Price = inputModel.Price
        };

        return await _repository.InsertAsync(newSalonService);
    }

    public async Task<SalonService> UpdateAsync(int id, EditSalonServiceModel editModel)
    {
        var serviceEdit = await _repository.GetByIdAsync(id);

        if (serviceEdit is null) return null;

        serviceEdit.Name = editModel.Name;
        serviceEdit.Category = editModel.Category;
        serviceEdit.Price = editModel.Price;

        serviceEdit = await _repository.UpdateAsync(serviceEdit, editModel.TenantId);

        return serviceEdit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var service = await _repository.GetByIdAsync(id);
        if (service is null) return false;

        return await _repository.DeleteAsync(service.Id);
    }
}
