using HRMS.Interfaces.Services;
using HRMS.Models;

namespace HRMS.Services.Impelmentation
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentServices(IDepartmentRepository deptRepo)
        {
            _deptRepo = deptRepo;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var includes = new string[] { "Manager" };
            
            return await _deptRepo.FindAllAsync(criteria: null, includes: includes);
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            var includes = new string[] { "Manager" };
            return await _deptRepo.FindAsync(d => d.DepartmentID == id, includes);
        }

        public async Task<Department> AddAsync(Department department)
        {
            return await _deptRepo.AddAsync(department);
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            try
            {
                await _deptRepo.UpdateAsync(department);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _deptRepo.GetByIdAsync(id);
            if (department == null)
            {
                return false;
            }

            try
            {
                await _deptRepo.DeleteAsync(department);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
