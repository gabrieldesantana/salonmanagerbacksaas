using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        var appointments = await _repository.GetAllAsync();

        if (appointments is null) return new List<Appointment>();

        return appointments;
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        if (id == 999)
        {
            for(var i = 0; i< 11; i++)
            {
                var customer = new Customer{Name = $"Cliente X{i}"};
                var service = new SalonService{ Name = $"Serviço X{i}"};
                var entryDate = DateTime.Now.AddDays(i);

                await _repository.InsertAsync(new Appointment{CustomerAppointment = customer, ServiceAppointment = service,  EntryDate = entryDate});
            }
        }

        var appointment = await _repository.GetByIdAsync(id);

        if (appointment is not null) return appointment;

        return null;
    }
    
    public async Task<bool> UpdateStatusAsync(Appointment appointment)
    {
        try
        {
            appointment.UpdateStatus();

            await _repository.UpdateAsync(appointment);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }




    public async Task<Appointment> InsertAsync(InputAppointmentModel inputModel)
    {

        var newAppointment = new Appointment
        {
            CustomerAppointment = inputModel.CustomerAppointment,
            ServiceAppointment = inputModel.ServiceAppointment,
            EntryDate = inputModel.EntryDate,
            Value = inputModel.Value
        };

        return await _repository.InsertAsync(newAppointment);
    }

    public async Task<Appointment> UpdateAsync(int id, EditAppointmentModel editModel)
    {
        var appointmentEdit = await _repository.GetByIdAsync(id);

        if (appointmentEdit is null) return null;

        appointmentEdit.ServiceAppointment = editModel.ServiceAppointment;
        appointmentEdit.EntryDate = editModel.EntryDate;
        appointmentEdit.Value = editModel.Value;

        appointmentEdit = await _repository.UpdateAsync(appointmentEdit);

        return appointmentEdit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var appointment = await _repository.GetByIdAsync(id);
        if (appointment is null) return false;

        return await _repository.DeleteAsync(appointment.Id);
    }
}
