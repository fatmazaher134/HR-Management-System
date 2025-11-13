using HRMS.ViewModels;
using HRMS.ViewModels.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HRMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.GetUserAsync(User);

            // TODO: In the future, inject IEmployeeService and ILeaveService 
            // to fetch real data from the database.

            var model = new DashboardViewModel
            {
                // Mock Data for now
                LeaveBalance = 21,
                ApprovedLeavesThisYear = 4,
                TotalEmployees = 150,
                PendingLeaveRequests = 8,
                NewHiresThisMonth = 3
            };

            return View(model);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
