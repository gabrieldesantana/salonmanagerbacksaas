using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment> GetByIdAsync(int id);

    Task<bool> UpdateStatusAsync(Appointment model);
    Task<Appointment> InsertAsync(InputAppointmentModel inputModel);
    Task<Appointment> UpdateAsync(int id, EditAppointmentModel editModel);
    Task<bool> DeleteAsync(int id);
}
