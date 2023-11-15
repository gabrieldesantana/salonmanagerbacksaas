using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task Rollback();

    }
}
