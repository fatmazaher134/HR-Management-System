using HRMS.Data;
using HRMS.Interfaces;
using HRMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

<<<<<<< HEAD
    public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.JobTitle)
            .Where(e => e.IsActive)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task SoftDeleteAsync(int employeeId)
    {
        var employee = await _context.Employees.FindAsync(employeeId);
        if (employee != null)
        {
            employee.IsActive = false;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }

    }
=======
    public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
    {
        
        return await _dbSet
            .Where(e => e.DepartmentID == departmentId)
            .ToListAsync();
    }
}
>>>>>>> origin/test

