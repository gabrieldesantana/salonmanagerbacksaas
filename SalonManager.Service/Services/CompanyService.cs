﻿using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private const string tenantIdAdmin = "80719";
        public CompanyService(ICompanyRepository repository) 
        {
            _repository = repository;
        }

        public async Task<List<Company>> GetAllAsync(string tenantId = "")
        {
            var companies = await ((tenantId != tenantIdAdmin) 
                ? _repository.GetAllAsync()
                : _repository.GetAllByTenantIdAsync(tenantId));

            if (companies is null) return new List<Company>();

            return companies;
        }

        public async Task<Company> GetByIdAsync(int id, string tenantId = "")
        {
            var company = await ((tenantId != tenantIdAdmin) 
                ? _repository.GetByIdAsync(id) 
                : _repository.GetByIdByTenantIdAsync(id, tenantId));

            if (company is null) return null;
            return company;
        }

        public async Task<Company> InsertAsync(InputCompanyModel inputModel)
        {

            var company = new Company
            {
                Name = inputModel.Name,
                TenantId = inputModel.TenantId,
                UserCreatorId = inputModel.UserCreatorId
            };

            return await _repository.InsertAsync(company);
        }

        public async Task<Company> UpdateAsync(int id, EditCompanyModel editModel)
        {
            var companyEdit = await _repository.GetByIdAsync(id);

            if (companyEdit is null) return null;

            companyEdit.Name = editModel.Name;
            companyEdit.CNPJ = editModel.CNPJ;

            return await _repository.UpdateAsync(companyEdit, editModel.TenantId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await _repository.GetByIdAsync(id);
            if (company is null) return false;

            return await _repository.DeleteAsync(company.Id);
        }
    }
}
