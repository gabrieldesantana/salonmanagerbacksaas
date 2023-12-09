using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IAppointmentRepository _appointmentRepository;
    public CustomerService(ICustomerRepository repository, IAppointmentRepository appointmentRepository)
    {
        _repository = repository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<List<Customer>> GetAllAsync(string tenantId = "")
    {
        List<Customer>? customers;

        if (tenantId == "80719")
            customers = await _repository.GetAllAsync();
        else
            customers = await _repository.GetAllByTenantIdAsync(tenantId);

        if (customers is null) return new List<Customer>();

        return customers;
    }

    public async Task<Customer> GetByIdAsync(int id, string tenantId = "")
    {

        Customer? customerEdit;

        if (tenantId == "80719")
            customerEdit = await _repository.GetByIdAsync(id);
        else
            customerEdit = await _repository.GetByIdByTenantIdAsync(id, tenantId);

       
        var appointments = await _appointmentRepository.GetByCustomerIdAsync(id);

        if (customerEdit is not null)
        {
            customerEdit.Appointments = appointments;

            if (appointments != null && appointments.Any())
            {
                var lastAppointment = appointments.Where(x => x.Date < DateTime.Now  && x.Status == Domain.Enums.EAppointmentStatus.Finalizado).MaxBy(x => x.Date);
                if (lastAppointment != null)
                {
                    customerEdit.LastService = lastAppointment.ServiceAppointment.Name;
                    customerEdit.LastServiceDate = lastAppointment.Date;
                }
              
            }

            customerEdit = await _repository.UpdateAsync(customerEdit, tenantId);

            return customerEdit;
        }


        return null;
    }

    public async Task<Customer> InsertAsync(InputCustomerModel inputModel)
    {
        var newCustomer = new Customer
        {
            UserCreatorId = inputModel.UserCreatorId,
            TenantId = inputModel.TenantId.ToString(),
            Cpf = inputModel.Cpf,
            Name = inputModel.Name,
            Nickname = inputModel.Nickname,
            Gender = inputModel.Gender,
            BirthDate = inputModel.BirthDate,
            PhoneNumber = inputModel.PhoneNumber,
            LastService = inputModel.LastService,
            LastServiceDate = inputModel.LastServiceDate,
            Times = 0
        };

        return await _repository.InsertAsync(newCustomer);
    }

    public async Task<Customer> UpdateAsync(int id, EditCustomerModel editModel)
    {
        var customerEdit = await _repository.GetByIdAsync(id);

        if (customerEdit is null) return null;

        customerEdit.Name = editModel.Name;
        customerEdit.Nickname = editModel.Nickname;
        customerEdit.PhoneNumber = editModel.PhoneNumber;
        customerEdit.Cpf= editModel.Cpf;
        customerEdit.BirthDate= editModel.BirthDate;

        return await _repository.UpdateAsync(customerEdit, editModel.TenantId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer is null) return false;

        return await _repository.DeleteAsync(customer.Id);
    }
}
