﻿using SalonManager.Domain.Entities;
using SalonManager.Domain.Enums;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly ISalonServiceRepository _serviceRepository;
    private readonly ICustomerRepository _customerRepository;
    private const string tenantIdAdmin = "80719";

    public AppointmentService(IAppointmentRepository repository, ICustomerRepository customerRepository, ISalonServiceRepository serviceRepository)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<List<Appointment>> GetAllAsync(string tenantId = "")
    {

        var appointments = await (tenantId == tenantIdAdmin
            ? _repository.GetAllAsync()
            : _repository.GetAllByTenantIdAsync(tenantId));

        if (!appointments.Any()) 
            return new List<Appointment>();

        foreach (var appointment in appointments) { appointment.ValidateStatus(); }

        return appointments;
    }

    public async Task<Appointment> GetByIdAsync(int id, string tenantId = "")
    {

        var appointment = await (tenantId == tenantIdAdmin
            ?  _repository.GetByIdAsync(id)
            : _repository.GetByIdByTenantIdAsync(id, tenantId));

        if (appointment is null) return null;

        return appointment;
    }

    public async Task<List<Appointment>> GetByCustomerIdAsync(int customerId)
    {
        var appointments = await _repository.GetByCustomerIdAsync(customerId);

        if (!appointments.Any()) return new List<Appointment>();

        return appointments;
    }

    //public async Task<List<Appointment>> GetByCustomerIdAsync(int customerId)
    //{
    //    var appointments = await _repository.GetByCustomerIdAsync(customerId);

    //    if (!appointments.Any()) return new List<Appointment>();

    //    return appointments;
    //}

    public async Task<FinanceAppointmentViewModel> GetFinishedByDateAsync(FinanceAppointmentModel financeModel)
    {
        var appointmentsFinished = await _repository.GetFinishedByDateAsync(financeModel);

        //if (!appointmentsFinished.Any())
        //    return ;

        appointmentsFinished ??= new();

        var totalValue = appointmentsFinished.Sum(x => x.Cost);

        return new FinanceAppointmentViewModel
        (
        appointmentsFinished,
        totalValue,
        financeModel.StartDate,
        financeModel.EndDate,
        financeModel.StartDate.ToString("dd/MM/yyyy"),
        financeModel.EndDate.ToString("dd/MM/yyyy")
        );
    }


    public async Task<Appointment> InsertAsync(InputAppointmentModel inputModel)
    {

        var existingCustomer = await _customerRepository.GetByIdAsync(inputModel.CustomerAppointmentId);
        var existingService = await _serviceRepository.GetByIdAsync(inputModel.ServiceAppointmentId);
        Employee existingEmployee = new(); //temp

        if (existingCustomer == null || existingService == null)
            return null;

        var newAppointment = new Appointment(existingCustomer, existingService, existingEmployee)
        {
            UserCreatorId = inputModel.UserCreatorId,
            TenantId = inputModel.TenantId.ToString(),
            Description = inputModel.Description,
            Date = DateTime.SpecifyKind(inputModel.Date, DateTimeKind.Unspecified)
        };

        return await _repository.InsertAsync(newAppointment);
    }

    public async Task<bool> UpdateAsync(int id, EditAppointmentModel editModel)
    {
        var appointmentEdit = await _repository.GetByIdCleanAsync(id);

        if (appointmentEdit == null) return false;

        appointmentEdit.Status = EAppointmentStatus.Remarcado;
        appointmentEdit.Date = editModel.Date;

        appointmentEdit = await _repository.UpdateAsync(appointmentEdit, editModel.TenantId);

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

            await _repository.UpdateAsync(appointmentEdit, editModel.TenantId);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> FinishAppointmentAsync(int id, FinishAppointmentModel finishModel)
    {
        var appointmentFinish = await _repository.GetByIdAsync(id);

        if (appointmentFinish == null) return false;

        appointmentFinish.PaymentMethod = finishModel.PaymentMethod;

        appointmentFinish.PaymentWay = (finishModel.PaymentWay == null) ? "A vista" : finishModel.PaymentWay;

        if (finishModel.PaymentMethod == "Pix" || finishModel.PaymentMethod == "Dinheiro")
            appointmentFinish.PaymentWay = "A Vista";

        appointmentFinish.Cost = finishModel.Cost;
        appointmentFinish.Status = EAppointmentStatus.Finalizado;
        appointmentFinish.Finished = true;
        appointmentFinish.FinishedDate = DateTime.Now;
        appointmentFinish.CustomerAppointment.IncreaseTimes();
        appointmentFinish.ServiceAppointment.Price = finishModel.Cost;
        appointmentFinish.Description = finishModel.Description;

        appointmentFinish = await _repository.UpdateAsync(appointmentFinish, finishModel.TenantId);

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
