using HRMS.Interfaces.Services;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        //  عرض كل الموظفين
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeServices.GetAllAsync();
            return View(employees);
        }

        // عرض تفاصيل موظف
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeServices.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        //  صفحة إنشاء موظف جديد (GET)
        public IActionResult Create()
        {
            return View();
        }

        //  حفظ الموظف الجديد (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            await _employeeServices.AddAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        //  صفحة التعديل (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeServices.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        //  حفظ التعديلات (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return View(employee);

            var updated = await _employeeServices.UpdateAsync(employee);
            if (!updated)
            {
                ModelState.AddModelError("", "Error updating employee.");
                return View(employee);
            }

            return RedirectToAction(nameof(Index));
        }

        //  صفحة تأكيد الحذف (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeServices.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        //  تنفيذ الحذف (Soft Delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeServices.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // الموظفين حسب القسم
        public async Task<IActionResult> ByDepartment(int departmentId)
        {
            var employees = await _employeeServices.GetByDepartmentIdAsync(departmentId);
            return View("Index", employees);
        }

        //  إجمالي المرتب حسب القسم
        public async Task<IActionResult> TotalSalary(int departmentId)
        {
            var totalSalary = await _employeeServices.GetTotalSalaryAsync(departmentId);
            ViewBag.DepartmentId = departmentId;
            ViewBag.TotalSalary = totalSalary;
            return View();
        }
    }
}

