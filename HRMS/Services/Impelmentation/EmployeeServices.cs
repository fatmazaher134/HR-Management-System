using HRMS.Interfaces;
using HRMS.Interfaces.Services;
using HRMS.Models;

namespace HRMS.Services.Impelmentation
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            // بنرجع الموظفين النشطين فقط (Soft Delete)
            return await _unitOfWork.Employee
                .FindAllAsync(e => e.IsActive, new[] { "Department", "JobTitle" });
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Employee
                .FindAsync(e => e.EmployeeID == id && e.IsActive,
                           new[] { "Department", "JobTitle" });
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _unitOfWork.Employee.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var existing = await _unitOfWork.Employee.GetByIdAsync(employee.EmployeeID);
            if (existing == null || !existing.IsActive)
                return false;

            // تحديث البيانات المطلوبة فقط
            existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;
            existing.Email = employee.Email;
            existing.PhoneNumber = employee.PhoneNumber;
            existing.BasicSalary = employee.BasicSalary;
            existing.Address = employee.Address;
            existing.DepartmentID = employee.DepartmentID;
            existing.JobTitleID = employee.JobTitleID;
            existing.DateOfBirth = employee.DateOfBirth;

            await _unitOfWork.Employee.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _unitOfWork.Employee.GetByIdAsync(id);
            if (emp == null)
                return false;

            emp.IsActive = false; // Soft delete
            await _unitOfWork.Employee.UpdateAsync(emp);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _unitOfWork.Employee
                .FindAllAsync(e => e.DepartmentID == departmentId && e.IsActive,
                              new[] { "Department", "JobTitle" });
        }

        public async Task<decimal> GetTotalSalaryAsync(int departmentId)
        {
            var employees = await _unitOfWork.Employee
                .FindAllAsync(e => e.DepartmentID == departmentId && e.IsActive);
            return employees.Sum(e => e.BasicSalary);
        }
    }
}
