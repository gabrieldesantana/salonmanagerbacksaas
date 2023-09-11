using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services;

public class FinanceService : IFinanceService
{
    private readonly IFinanceRepository _repository;
    public FinanceService(IFinanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Finance>> GetAllAsync()
    {
        var finances = await _repository.GetAllAsync();

        await _repository.InsertAsync(new Finance());

        if (finances is null) return new List<Finance>();

        return finances;
    }

    public async Task<Finance> GetByIdAsync(int id)
    {
        var finance = await _repository.GetByIdAsync(id);

        if (finance is not null) return finance;

        return null;
    }

    public async Task<Finance> InsertAsync(InputFinanceModel inputModel)
    {
        var newFinance = new Finance
        {
            Name = inputModel.Name,
            Value = inputModel.Value,
            EntryDate = inputModel.EntryDate,
            PaymentType = inputModel.PaymentType
        };

        return await _repository.InsertAsync(newFinance);
    }

    public async Task<Finance> UpdateAsync(int id, EditFinanceModel editModel)
    {
        var financeEdit = await _repository.GetByIdAsync(id);

        if (financeEdit is null) return null;

        financeEdit.Name = editModel.Name;

        financeEdit = await _repository.UpdateAsync(financeEdit);

        return financeEdit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var finance = await _repository.GetByIdAsync(id);
        if (finance is null) return false;

        return await _repository.DeleteAsync(finance.Id);
    }
}
