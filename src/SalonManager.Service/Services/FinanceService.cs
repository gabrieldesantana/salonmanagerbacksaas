using SalonManager.Domain.Entities;
using SalonManager.Domain.Enums;
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

        var customer = new Customer
        {
            Name = $"Nome X",
            Nickname = $"Apelido X",
            Cpf = $"222.333.444-88",
            Gender = "Masculino",
            PhoneNumber = "79998738234",
            BirthDate = DateTime.Now,
            Appointments= new List<Appointment> {},
            LastService = "service",
            LastServiceDate= DateTime.Now,
            Times = 0
        };
        var service = new SalonService
        {
            Name = $"Nome X",
            Category = $"Categoria X",
            Actived = true,
            Price = 100,
            HaveTax = "Não",
            Tax = 0
        };

        await _repository.InsertAsync(new Finance
        {
            Type = "Entrada",
            Description = "Selagem",
            Appointment = new Appointment 
            {
                CustomerAppointment = customer,
                ServiceAppointment = service,
                Description = "N/A",
                Date = DateTime.Now,
                Status = EAppointmentStatus.Iniciado,
                Value = 130
            },
            Value = 130,
            PaymentType = "PIX",
            EntryDate = DateTime.MinValue
        }
        );

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
            Type = inputModel.Type,
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

        financeEdit.Type = editModel.Type;

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
