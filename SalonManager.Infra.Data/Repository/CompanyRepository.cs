using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;

namespace SalonManager.Infra.Data.Repository
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork) 
        {
        
        }

        public override async Task<Company> GetByIdAsync(int id)
        {
            return await _context.Companies
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
