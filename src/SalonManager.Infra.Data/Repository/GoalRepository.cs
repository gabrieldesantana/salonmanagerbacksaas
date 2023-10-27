
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;

namespace SalonManager.Infra.Data.Repository;
public class GoalRepository : GenericRepository<Goal> , IGoalRepository
{
    public GoalRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }
}
