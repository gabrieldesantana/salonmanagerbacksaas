using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;

namespace SalonManager.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        public CompanyService(ICompanyRepository repository) 
        {
            _repository = repository;
        }

        public async Task<List<Company>> GetAllAsync()
        {
            var companies = await _repository.GetAllAsync();
            if (companies is null) return new List<Company>();

            return companies;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            var company = await _repository.GetByIdAsync(id);

            if (company is null) return null;
            return company;
        }

        public async Task<Company> InsertAsync(InputCompanyModel inputModel)
        {

            var company = new Company
            {
                Name = inputModel.Name,
                CNPJ = inputModel.CNPJ,
                TenantId = Guid.NewGuid().ToString()
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
