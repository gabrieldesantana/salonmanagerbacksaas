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

    public async Task<List<SalonService>> GetAllAsync()
    {
        var services = await _repository.GetAllAsync();

        if (services is null) return new List<SalonService>();

        return services;
    }

    public async Task<SalonService> GetByIdAsync(int id)
    {
        if (id == 999)
        {
            for(var i = 0; i< 11; i++)
            {
                await _repository.InsertAsync(new SalonService 
                {
                    Name = $"Nome X{i}",
                    Category = $"Categoria X{i}",
                    Actived = true,
                    Price = 100 + i
                }
                );
            }
        }
        var service = await _repository.GetByIdAsync(id);

        if (service is not null) return service;

        return null;
    }

    public async Task<SalonService> InsertAsync(InputSalonServiceModel inputModel)
    {
        var newSalonService = new SalonService
        {
            Name = inputModel.Name,
            Category = inputModel.Category,
            Price = inputModel.Price,
            HaveTax = inputModel.HaveTax,
            Tax = inputModel.Tax
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
        serviceEdit.HaveTax = editModel.HaveTax;
        serviceEdit.Tax = editModel.Tax;

        serviceEdit = await _repository.UpdateAsync(serviceEdit);

        return serviceEdit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var service = await _repository.GetByIdAsync(id);
        if (service is null) return false;

        return await _repository.DeleteAsync(service.Id);
    }
}
