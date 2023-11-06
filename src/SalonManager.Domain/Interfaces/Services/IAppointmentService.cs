using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync(string tenantId);
    Task<Appointment> GetByIdAsync(int id, string tenantId);
    Task<List<Appointment>> GetByCustomerIdAsync(int customerId);

    Task<FinanceAppointmentViewModel> GetFinishedByDateAsync(FinanceAppointmentModel financeModel);

    Task<bool> UpdateStatusAsync(int id, EditAppointmentModel editModel);
    Task<Appointment> InsertAsync(InputAppointmentModel inputModel);
    Task<bool> UpdateAsync(int id, EditAppointmentModel editModel);
    Task<bool> FinishAppointmentAsync(int id, FinishAppointmentModel finishModel);
    Task<bool> DeleteAsync(int id);
}
