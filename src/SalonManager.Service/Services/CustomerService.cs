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

    public async Task<List<Customer>> GetAllAsync()
    {
        var customers = await _repository.GetAllAsync();

        if (customers is null) return new List<Customer>();

        return customers;
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        if (id == 999)
        {
            for(var i = 0; i< 11; i++)
            {
                var c = new Customer 
                {
                    Name = $"Nome X{i}",
                    Nickname = $"Apelido X{i}",
                    Cpf = $"{i + i}",
                    PhoneNumber = "79998738234",
                    BirthDate = DateTime.Now
                };

                await _repository.InsertAsync(c);
            }
        }



        var customerEdit = await _repository.GetByIdAsync(id);
        var appointments = await _appointmentRepository.GetByCustomerIdAsync(id);

        if (customerEdit is not null)
        {
            customerEdit.Appointments = appointments;
            if (appointments != null && appointments.Any())
            {
                var lastAppointment = appointments.Where(x => x.Date < DateTime.Now).MaxBy(x => x.Date);
                if (lastAppointment != null)
                {
                    customerEdit.LastService = lastAppointment.ServiceAppointment.Name;
                    customerEdit.LastServiceDate = lastAppointment.Date;
                }
              
            }

            customerEdit = await _repository.UpdateAsync(customerEdit);

            return customerEdit;
        }


        return null;
    }

    public async Task<Customer> InsertAsync(InputCustomerModel inputModel)
    {
        var newCustomer = new Customer
        {
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

        return await _repository.UpdateAsync(customerEdit);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer is null) return false;

        return await _repository.DeleteAsync(customer.Id);
    }
}
