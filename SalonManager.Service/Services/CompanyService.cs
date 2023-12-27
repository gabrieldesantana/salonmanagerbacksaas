using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;
using SalonManager.Application.Helpers;
using System.Text.RegularExpressions;

namespace SalonManager.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        public CompanyService(ICompanyRepository repository) 
        {
            _repository = repository;
        }

        public async Task<List<Company>> GetAllAsync(string tenantId = "")
        {
            List<Company>? companies;

            if (tenantId == "80719")
                companies = await _repository.GetAllAsync();
            else
                companies = await _repository.GetAllByTenantIdAsync(tenantId);

            if (companies is null) return new List<Company>();

            return companies;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            var company = await _repository.GetByIdAsync(id);
            if (company != null) return company;
            return null;
        }

        public async Task<Company> GetByLoginAsync(string login)
        {
            var company = await _repository.GetByLoginAsync(login);
            if (company != null) return company;
            return null;
        }

        public async Task<Company> InsertAsync(InputCompanyModel inputModel)
        {
            var company = new Company();
            // var company = "_companyRepository.GetByIdAsync(inputModel.CompanyId)";

            var company = new Company
            {
                Name = inputModel.Name,
                CompanyId = inputModel.CompanyId,
                Role = inputModel.Role,
                Login = inputModel.Login,
                Email = inputModel.Email,
                TenantId = company.TenantId //add
            };

            company.SetCreatorId(); //linkando o Id

            bool passwordOk = Regex.IsMatch(inputModel.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$");

            if (!passwordOk)
                return null;

            company.SetPasswordHash(inputModel.Password);

            return await _repository.InsertAsync(company);
        }

        public async Task<Company> UpdateAsync(int id, EditCompanyModel editModel)
        {
            var companyEdit = await _repository.GetByIdAsync(id);

            if (companyEdit is null) return null;

            companyEdit.Name = editModel.Name;
            companyEdit.CompanyName = editModel.CompanyName;
            companyEdit.Email = editModel.Email;
            companyEdit.Password = editModel.Password;

            return await _repository.UpdateAsync(companyEdit);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await _repository.GetByIdAsync(id);
            if (company is null) return false;

            return await _repository.DeleteAsync(company.Id);
        }
    }
}
