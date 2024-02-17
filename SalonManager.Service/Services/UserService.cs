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
        private readonly ICompanyRepository _companyRepository;
        public UserService(IUserRepository repository, ICompanyRepository companyRepository) 
        {
            _companyRepository = companyRepository;
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

            //var company = await _employeeRepository.GetCompanyByUserId();

            if (user is null) return null;
            return user;
        }

        public async Task<User> InsertAsync(InputUserModel inputModel)
        {

            var company = await _companyRepository.GetByIdAsync(inputModel.CompanyId);

            if (company is null)
                return null;

            var user = new User
            {
                Name = inputModel.Name,
                Cpf = inputModel.CPF,
                Role = inputModel.Role,
                Login = inputModel.Login,
                Email = inputModel.Email,
                TenantId = company.TenantId,
                //Company = company,
                // CompanyId = company.Id,
                //CompanyAdministrator = inputModel.CompanyAdministrator
            };

            bool passwordOk = Regex.IsMatch(inputModel.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$");

            if (!passwordOk)
                return null;

            user.SetPasswordHash(inputModel.Password);

            var newUser = await _repository.InsertAsync(user);

            newUser.SetCreatorId();

            await _repository.UpdateAsync(newUser);

            return newUser;
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
