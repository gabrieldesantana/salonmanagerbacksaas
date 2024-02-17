using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Infra.Data.Context;
using SalonManager.Infra.Data.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonManager.Infra.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRepository(SalonManagerDbContext context, IUnitOfWork unitOfWork)
            : base(context, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await _context.Users
                //.Include(x => x.Company)
                .FirstOrDefaultAsync((x => x.Login.ToLower() == login.ToLower()));
        }

        public async Task<User> UpdateAsync(User user)
        {
            var result = await GetByIdAsync(user.Id);

            if (result is not null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    await _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    await _unitOfWork.Rollback();
                }
            }
            return user;
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
