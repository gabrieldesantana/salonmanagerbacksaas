﻿using System.Text.Json.Serialization;

namespace SalonManager.Domain.Entities;

public class Customer : BaseEntity
{
    public string? Cpf { get; set; }
    public string? Name { get; set; }
    public string? Nickname { get; set; }
    public string? Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? LastService { get; set; }
    public DateTime LastServiceDate { get; set; }
    public int Times { get; set; }
    public List<Appointment> Appointments { get; set; }

    public void IncreaseTimes()
    {
        this.Times += 1;
    }
}

public record InputCustomerModel
(
    int Id, string? Cpf, string? Name, string? Nickname, string? Gender, DateTime BirthDate, string PhoneNumber, string LastService, DateTime LastServiceDate, string TenantId, int UserCreatorId
);

public record EditCustomerModel 
(
    int Id, string? Name, string? Nickname, string PhoneNumber, string? Cpf, DateTime BirthDate, string TenantId
);


