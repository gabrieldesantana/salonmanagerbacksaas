using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment> GetByIdAsync(int id);
    Task<List<Appointment>> GetByCustomerIdAsync(int customerId);
    Task<bool> UpdateStatusAsync(int id, EditAppointmentModel editModel);
    Task<Appointment> InsertAsync(InputAppointmentModel inputModel);
    Task<bool> UpdateAsync(int id, EditAppointmentModel editModel);
    Task<bool> DeleteAsync(int id);
}
