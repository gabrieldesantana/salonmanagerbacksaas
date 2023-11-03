using SalonManager.Domain.Entities;
using SalonManager.Domain.Enums;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly ISalonServiceRepository _serviceRepository;
    private readonly ICustomerRepository _customerRepository;

    public AppointmentService(IAppointmentRepository repository, ICustomerRepository customerRepository, ISalonServiceRepository serviceRepository)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        var appointments = await _repository.GetAllAsync();
        foreach (var appointment in appointments) { appointment.ValidateStatus(); }

        if (appointments is null) return new List<Appointment>();

        return appointments;
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        if (id == 999)
        {
            for(var i = 0; i< 11; i++)
            {
                var customer = new Customer
                {
                    Name = $"Nome X{i}",
                    Nickname = $"Apelido X{i}",
                    Cpf = $"{i + i}",
                    Gender = "Masculino",
                    PhoneNumber = "79998738234",
                    BirthDate = DateTime.Now
                };
                var service = new SalonService
                {
                    Name = $"Nome X{i}",
                    Category = $"Categoria X{i}",
                    Actived = true,
                    Price = 100 + i
                };
                var entryDate = DateTime.Now.AddDays(i);

                await _repository.InsertAsync(new Appointment{CustomerAppointment = customer, ServiceAppointment = service,  Date = entryDate});
            }
        }

        var appointment = await _repository.GetByIdAsync(id);

        if (appointment is not null) return appointment;

        return null;
    }

    public async Task<List<Appointment>> GetByCustomerIdAsync(int customerId)
    {
        var appointments = await _repository.GetByCustomerIdAsync(customerId);

        if (appointments != null) return appointments;

        return null;
    }


    public async Task<Appointment> InsertAsync(InputAppointmentModel inputModel)
    {

        var existingCustomer = await _customerRepository.GetByIdAsync(inputModel.CustomerAppointmentId);
        var existingService = await _serviceRepository.GetByIdAsync(inputModel.ServiceAppointmentId);

        if (existingCustomer == null || existingService == null)
            return null;

        var newAppointment = new Appointment
        {
            CustomerAppointment = existingCustomer,
            ServiceAppointment = existingService,
            CustomerAppointmentId = existingCustomer.Id,
            ServiceAppointmentId = existingService.Id,
            Description = inputModel.Description,
            Date = inputModel.Date
            //Value = existingService.Price
        };

        return await _repository.InsertAsync(newAppointment);
    }

    public async Task<bool> UpdateAsync(int id, EditAppointmentModel editModel)
    {
        var appointmentEdit = await _repository.GetByIdCleanAsync(id);

        if (appointmentEdit == null) return false;

        appointmentEdit.Status = EAppointmentStatus.Remarcado;
        appointmentEdit.Date = editModel.Date;
        //appointmentEdit.Description = editModel.Description;


        appointmentEdit = await _repository.UpdateAsync(appointmentEdit);

        if (appointmentEdit == null)
            return false;

        return true;
    }

    public async Task<bool> UpdateStatusAsync(int id, EditAppointmentModel editModel)
    {
        try
        {
            var appointmentEdit = await _repository.GetByIdCleanAsync(id);
            if (appointmentEdit is null) return false;
            appointmentEdit.Status = editModel.Status;

            await _repository.UpdateAsync(appointmentEdit);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> FinishAppointmentAsync(int id, FinishAppointmentModel finishModel)
    {
        var appointmentFinish = await _repository.GetByIdCleanAsync(id);

        if (appointmentFinish == null) return false;

        appointmentFinish.PaymentMethod = finishModel.PaymentMethod;
        appointmentFinish.PaymentWay = finishModel.PaymentWay;
        appointmentFinish.Value = finishModel.Value;
        appointmentFinish.Status = EAppointmentStatus.Finalizado;
        appointmentFinish.Finished = true;

        appointmentFinish = await _repository.UpdateAsync(appointmentFinish);

        if (appointmentFinish == null)
            return false;

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var appointment = await _repository.GetByIdCleanAsync(id);
        if (appointment is null) return false;

        return await _repository.DeleteAsync(appointment.Id);
    }
}
