using SalonManager.Domain.Entities;

namespace SalonManager.Domain.Interfaces.Services;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByLoginAsync(string login);
    Task<User> InsertAsync(InputUserModel inputModel);
    Task<User> UpdateAsync(int id, EditUserModel editModel);
    Task<bool> DeleteAsync(int id);

    //Token ValidateCredentials(User user);
    //Token ValidateCredentials(Token token);

    //bool RevokeToken(string username);

}
