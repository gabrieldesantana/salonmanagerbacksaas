using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Infra.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {

        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync((x => x.Login.ToLower() == login.ToLower()));
        }

        //public User RefreshUserInfo(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool RevokeToken(string username)
        //{
        //    throw new NotImplementedException();
        //}

        //public User ValidateCredential(UserVO userVO)
        //{
        //    throw new NotImplementedException();
        //}

        //public User ValidateCredential(string username)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
