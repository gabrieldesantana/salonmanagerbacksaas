using SalonManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Domain.Interfaces.Services
{
    public interface IGoalService
    {
        Task<List<Goal>> GetAllAsync();
        Task<Goal> GetByIdAsync(int id);
        Task<Goal> InsertAsync(InputGoalModel inputModel);
        Task<Goal> UpdateAsync(int id, EditGoalModel editModel);
        Task<bool> DeleteAsync(int id);
    }

}
