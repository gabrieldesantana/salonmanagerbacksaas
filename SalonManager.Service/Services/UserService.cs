using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;
using SalonManager.Application.Helpers;
using System.Text.RegularExpressions;

namespace SalonManager.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) 
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user is null) return null;
            return user;
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            var user = await _repository.GetByLoginAsync(login);
            if (user is null) return null;
            return user;
        }

        public async Task<User> InsertAsync(InputUserModel inputModel)
        {
            var company = new Company();
            // var company = "_companyRepository.GetByIdAsync(inputModel.CompanyId)";

            var user = new User
            {
                Name = inputModel.Name,
                Role = inputModel.Role,
                Login = inputModel.Login,
                Email = inputModel.Email,
                TenantId = company.TenantId //add
            };

            user.SetCreatorId(); //linkando o id

            bool passwordOk = Regex.IsMatch(inputModel.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$");

            if (!passwordOk)
                return null;

            user.SetPasswordHash(inputModel.Password);

            return await _repository.InsertAsync(user);
        }

        public async Task<User> UpdateAsync(int id, EditUserModel editModel)
        {
            var userEdit = await _repository.GetByIdAsync(id);

            if (userEdit is null) return null;

            userEdit.Name = editModel.Name;
            userEdit.Email = editModel.Email;
            userEdit.Password = editModel.Password;

            return await _repository.UpdateAsync(userEdit);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user is null) return false;

            return await _repository.DeleteAsync(user.Id);
        }
    }
}
