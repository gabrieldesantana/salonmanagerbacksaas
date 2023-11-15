using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Infra.Data.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly SalonManagerDbContext _context;
    public UnitOfWork(SalonManagerDbContext context)
    {
        _context = context;
    }
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Rollback()
    {
        await _context.DisposeAsync();
    }
}
