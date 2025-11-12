using HRMS.ViewModels.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestServices _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaveRequestController(ILeaveRequestServices service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> MyRequests()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = await _service.GetMyRequestsAsync(user.Id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveRequestViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(model, user.Id);
                return RedirectToAction(nameof(MyRequests));
            }
            return View(model);
        }

        public async Task<IActionResult> Approve(int id)
        {
            // لاحقًا هنجيب HR ID من المستخدم الحالي
            await _service.ApproveAsync(id, 1);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int id)
        {
            await _service.RejectAsync(id, 1, "Not eligible");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await _service.CancelAsync(id, 1);
            return RedirectToAction(nameof(MyRequests));
        }
    }
}
