﻿using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Repository;
public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<List<Appointment>> GetByCustomerIdAsync(int customerId);
    Task<Appointment> GetByIdCleanAsync(int id);
}
