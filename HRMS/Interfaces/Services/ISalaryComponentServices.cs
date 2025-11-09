using HRMS.Models;

namespace HRMS.Interfaces.Services
{
    public interface ISalaryComponentServices
    {
        Task<IEnumerable<SalaryComponent>> GetAllAsync();
        Task<SalaryComponent?> GetByIdAsync(int id);
        Task<SalaryComponent> AddAsync(SalaryComponent component);
        Task<bool> UpdateAsync(SalaryComponent component);
        Task<bool> DeleteAsync(int id);
    }

}
