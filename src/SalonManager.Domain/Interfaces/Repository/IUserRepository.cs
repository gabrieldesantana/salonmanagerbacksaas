using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //User ValidateCredential(UserVO userVO);
        //User ValidateCredential(string username);
        //bool RevokeToken(string username);
        //User RefreshUserInfo(User user);
        Task<User> GetByLoginAsync(string login);
    }
}
