using HRMS.Interfaces;
using HRMS.Interfaces.Services;
using HRMS.Models;

namespace HRMS.Services.Impelmentation
{
    public class EmployeeServices : IEmployeeServices
    {
<<<<<<< HEAD
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
=======
        private readonly IEmployeeRepository _empRepo;

        public EmployeeServices(IEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }


        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var includes = new string[] { "Department", "JobTitle" };            
            return await _empRepo.FindAllAsync(criteria: null, includes: includes);
>>>>>>> origin/test
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
<<<<<<< HEAD
            return await _unitOfWork.Employee
                .FindAsync(e => e.EmployeeID == id && e.IsActive,
                                                        new[] { "Department", "JobTitle" });
=======
            var includes = new string[] { "Department", "JobTitle" };
            return await _empRepo.FindAsync(e => e.EmployeeID == id, includes);
>>>>>>> origin/test
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
<<<<<<< HEAD
            await _unitOfWork.Employee.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
            return employee;
=======
            return await _empRepo.AddAsync(employee);
>>>>>>> origin/test
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
<<<<<<< HEAD
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
=======
            try
            {
                await _empRepo.UpdateAsync(employee);
                return true;
            }
            catch
            {
                return false;
            }
>>>>>>> origin/test
        }

        public async Task<bool> DeleteAsync(int id)
        {
<<<<<<< HEAD
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
=======
            var employee = await _empRepo.GetByIdAsync(id);
            if (employee == null)
            {
                return false; 
            }

            try
            {
                await _empRepo.DeleteAsync(employee);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _empRepo.GetEmployeesByDepartmentAsync(departmentId);
>>>>>>> origin/test
        }

        public async Task<decimal> GetTotalSalaryAsync(int departmentId)
        {
<<<<<<< HEAD
            var employees = await _unitOfWork.Employee
                .FindAllAsync(e => e.DepartmentID == departmentId && e.IsActive);
            return employees.Sum(e => e.BasicSalary);
=======
          
            var employees = await _empRepo.FindAllAsync(
                criteria: e => e.DepartmentID == departmentId,
                includes: null 
            );

            if (employees.Any())
            {
                return employees.Sum(e => e.BasicSalary);
            }

            return 0;
>>>>>>> origin/test
        }
    }
}

