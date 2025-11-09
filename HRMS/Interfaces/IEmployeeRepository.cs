using HRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{

    Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
    Task SoftDeleteAsync(int employeeId);
}