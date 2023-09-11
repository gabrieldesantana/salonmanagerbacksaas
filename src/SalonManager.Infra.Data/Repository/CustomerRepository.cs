
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;

namespace SalonManager.Infra.Data.Repository;
public class CustomerRepository : GenericRepository<Customer> , ICustomerRepository
{
    public CustomerRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }
}
