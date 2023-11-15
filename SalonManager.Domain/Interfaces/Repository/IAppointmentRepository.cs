using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Repository;
public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<List<Appointment>> GetByCustomerIdAsync(int customerId);
    Task<Appointment> GetByIdAsync(int id);
    Task<Appointment> GetByIdCleanAsync(int id);

    Task<List<Appointment>> GetAllByTenantIdAsync(string tenantId);
    Task<Appointment> GetByIdByTenantIdAsync(int id, string tenantId);

    Task<List<Appointment>> GetFinishedByDateAsync(FinanceAppointmentModel financeModel);
}
