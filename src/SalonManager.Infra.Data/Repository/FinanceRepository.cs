using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;
using SalonManager.Infra.Data.Repository.UnitOfWork;

namespace SalonManager.Infra.Data.Repository
{
    public class FinanceRepository : GenericRepository<Finance>, IFinanceRepository
    {
        public FinanceRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {

        }

        public override async Task<List<Finance>> GetAllAsync()
        {
            return await _context.Finances
            .Where(x => x.Actived == true || x.Actived == false)
            .Include(p => p.Appointment)
            .Include(p => p.Appointment.CustomerAppointment)
            .Include(p => p.Appointment.ServiceAppointment)
            .ToListAsync();
        }
    }
}
