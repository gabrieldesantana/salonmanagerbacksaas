﻿using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;

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
            .AsNoTracking()
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
             .AsNoTracking()
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
            var startDate = DateTime.SpecifyKind(financeModel.StartDate, DateTimeKind.Unspecified);
            var endDate = DateTime.SpecifyKind(financeModel.EndDate, DateTimeKind.Unspecified);

            return await _context.Appointments
                .AsNoTracking()
                .Where(
                x => x.Date.Date >= startDate.Date &&
                x.Date.Date <= endDate.Date &&
                x.Status == Domain.Enums.EAppointmentStatus.Finalizado &&
                x.TenantId == financeModel.TenantId)
                .Include(p => p.CustomerAppointment)
                .Include(p => p.ServiceAppointment)
                .ToListAsync();
        }

    }
}
