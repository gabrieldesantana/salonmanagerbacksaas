using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        var customers = await _repository.GetAllAsync();

        await _repository.InsertAsync(new Customer());

        if (customers is null) return new List<Customer>();

        return customers;
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);

        if (customer is not null) return customer;

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
        customerEdit.Gender = editModel.Gender;
        customerEdit.BirthDate = editModel.BirthDate;
        customerEdit.PhoneNumber = editModel.PhoneNumber;

        customerEdit = await _repository.UpdateAsync(customerEdit);

        return customerEdit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer is null) return false;

        return await _repository.DeleteAsync(customer.Id);
    }
}
