using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;
using SalonManager.Infra.Data.Repository.UnitOfWork;

namespace SalonManager.Infra.Data.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {

        }

        public override async Task<List<Appointment>> GetAllByTenantIdAsync(string tenantId) 
        {
            var tenantIds = new List<string>() { tenantId };

            return await _context.Appointments
            .Where(x => x.Actived == true && tenantIds.Contains(x.TenantId))
            .Include(p => p.CustomerAppointment)
            .Include(p => p.ServiceAppointment)
            .ToListAsync();
        }
        public override async Task<Appointment> GetByIdByTenantIdAsync(int id, string tenantId) 
        {
            return await _context.Appointments
            .Where(x => x.TenantId == tenantId && x.Id == id)
            .Include(p => p.CustomerAppointment)
            .Include(p => p.ServiceAppointment)
            .FirstOrDefaultAsync();
        }


        public override async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
            .Where(x => x.Actived == true || x.Actived == false)
            .Include(p => p.CustomerAppointment)
            .Include(p => p.ServiceAppointment)
            .ToListAsync();
        }

        public override async Task<Appointment> GetByIdAsync(int id)
        {
            return await _context.Appointments
            .Include(p => p.CustomerAppointment)
            .Include(p => p.ServiceAppointment)
            .FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<Appointment> GetByIdCleanAsync(int id)
        {
            return await _context.Appointments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Appointment>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Appointments
                .Where(x => x.CustomerAppointmentId == customerId)
                .Include(p => p.CustomerAppointment)
                .Include(p => p.ServiceAppointment)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetFinishedByDateAsync(FinanceAppointmentModel financeModel)
        {
            return await _context.Appointments
                .Where(
                x => x.Date.Date >= financeModel.StartDate.Date &&
                x.Date.Date <= financeModel.EndDate.Date &&
                x.Status == Domain.Enums.EAppointmentStatus.Finalizado &&
                x.TenantId == financeModel.TenantId)
                .Include(p => p.CustomerAppointment)
                .Include(p => p.ServiceAppointment)
                .ToListAsync();
        }

    }
}
