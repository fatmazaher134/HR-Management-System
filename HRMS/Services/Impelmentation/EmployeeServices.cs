using HRMS.Interfaces;
using HRMS.Interfaces.Services;
using HRMS.Models;
using HRMS.ViewModels.Employee;

namespace HRMS.Services.Impelmentation
{
    public class EmployeeServices : IEmployeeServices
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public EmployeeServices(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //public async Task<IEnumerable<Employee>> GetAllAsync()
        //{
        //    // بنرجع الموظفين النشطين فقط (Soft Delete)
        //    return await _unitOfWork.Employee
        //        .FindAllAsync(e => e.IsActive, new[] { "Department", "JobTitle" });

        private readonly IEmployeeRepository _empRepo;

        public EmployeeServices(IEmployeeRepository empRepo)
        {
            _empRepo = empRepo;
        }


        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var includes = new string[] { "Department", "JobTitle", "ApplicationUser" };
            return await _empRepo.FindAllAsync(
                criteria: e => e.IsActive,
                includes: includes
            );

            //var includes = new string[] { "Department", "JobTitle" };            
            //return await _empRepo.FindAllAsync(criteria: null, includes: includes);

        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var includes = new string[] { "Department", "JobTitle", "ApplicationUser" };
            return await _empRepo.FindAsync(
                criteria: e => e.EmployeeID == id && e.IsActive,
                includes: includes
            );
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            employee.IsActive = true;
            employee.HireDate = employee.HireDate == default ? DateTime.Now : employee.HireDate;
            //await _unitOfWork.Employee.AddAsync(employee);
            //await _unitOfWork.SaveChangesAsync();
            //return employee;

            return await _empRepo.AddAsync(employee);

        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            try
            {
                var existing = await _empRepo.GetByIdAsync(employee.EmployeeID);
                if (existing == null || !existing.IsActive)
                    return false;

                await _empRepo.UpdateAsync(employee);
                return true;
            }
            catch
            {
                return false;
            }
            //var existing = await _unitOfWork.Employee.GetByIdAsync(employee.EmployeeID);
            //if (existing == null || !existing.IsActive)
            //    return false;

            //// تحديث البيانات المطلوبة فقط
            //existing.FirstName = employee.FirstName;
            //existing.LastName = employee.LastName;
            //existing.Email = employee.Email;
            //existing.PhoneNumber = employee.PhoneNumber;
            //existing.BasicSalary = employee.BasicSalary;
            //existing.Address = employee.Address;
            //existing.DepartmentID = employee.DepartmentID;
            //existing.JobTitleID = employee.JobTitleID;
            //existing.DateOfBirth = employee.DateOfBirth;

            //await _unitOfWork.Employee.UpdateAsync(existing);
            //await _unitOfWork.SaveChangesAsync();
            //return true;



        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                // Soft Delete: تغيير IsActive إلى false
                await _empRepo.SoftDeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
            //var employee = await _empRepo.GetByIdAsync(id);
            //if (employee == null)
            //{
            //    return false;
            //}

            //try
            //{
            //    await _empRepo.DeleteAsync(employee);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }


        public async Task<Employee?> GetByUserIdAsync(string userId)
        {
            // return emp based on UserId
            var includes = new string[] { "Department", "JobTitle", "ApplicationUser" };
            return await _empRepo.FindAsync(
                criteria: e => e.UserId == userId && e.IsActive,
                includes: includes
            );
        }

        public async Task<bool> UpdateBasicInfoAsync(int employeeId, EmployeeEditBasicInfoViewModel model)
        {
            try
            {
                // Cuurent Emp
                var employee = await _empRepo.GetByIdAsync(employeeId);
                if (employee == null || !employee.IsActive)
                    return false;

                // Edit ON Allowed Baseic Info
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Address = model.Address;

                await _empRepo.UpdateAsync(employee);
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
        }




        public async Task<decimal> GetTotalSalaryAsync(int departmentId)
        {
            var employees = await _empRepo.FindAllAsync(
                    criteria: e => e.DepartmentID == departmentId && e.IsActive,
                    includes: null
                );

            if (employees.Any())
            {
                return employees.Sum(e => e.BasicSalary);
            }

            return 0;

        }


        public async Task<bool> IsEmailExistsAsync(string email, int? excludeEmployeeId = null)
        {

            if (excludeEmployeeId.HasValue)
            {
                return await _empRepo.IsExistAsync(
                    e => e.Email == email && e.EmployeeID != excludeEmployeeId.Value
                );
            }

            return await _empRepo.IsExistAsync(e => e.Email == email);
        }
    }
 }


