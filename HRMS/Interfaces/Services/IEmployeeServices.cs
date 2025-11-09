using HRMS.Models;

namespace HRMS.Interfaces.Services
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);
        Task<decimal> GetTotalSalaryAsync(int departmentId);
    }

}
