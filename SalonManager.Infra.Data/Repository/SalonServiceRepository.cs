using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;
using SalonManager.Infra.Data.Repository.UnitOfWork;

namespace SalonManager.Infra.Data.Repository
{
    public class SalonServiceRepository : GenericRepository<SalonService>, ISalonServiceRepository
    {
        public SalonServiceRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {

        }
    }
}
