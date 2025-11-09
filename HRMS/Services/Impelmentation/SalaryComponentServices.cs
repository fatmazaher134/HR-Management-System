using HRMS.Interfaces.Services;
using HRMS.Models;

namespace HRMS.Services.Impelmentation
{
    public class SalaryComponentServices : ISalaryComponentServices
    {
        public Task<SalaryComponent> AddAsync(SalaryComponent component)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SalaryComponent>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SalaryComponent?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(SalaryComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
