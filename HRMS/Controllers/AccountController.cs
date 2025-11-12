using HRMS.Interfaces.Services;
using HRMS.Models;
using HRMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace HRMS.Controllers
{
    public class AccountController(IAccountService _accountService) : Controller
    {

        //Register Action
        [HttpGet]
        public async Task<IActionResult> LoginAsync()
        {
            await Logout();
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _accountService.LoginUserAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "This account has been locked out. Please try again later.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            List<string> Roles = _accountService.GetAllRoles();

            List<SelectListItem> roleOptions = Roles
                .Select(role => new SelectListItem { Text = role, Value = role })
                .ToList();

            RegisterViewModel registerViewModel = new()
            {
                Options = roleOptions
            };
            return View("Register", registerViewModel);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ModelState.Remove(nameof(model.Options));
            if (ModelState.IsValid)
            {

                var result = await _accountService.RegisterUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
                AddErrors(result);
            }

            List<string> Roles = _accountService.GetAllRoles();

            List<SelectListItem> roleOptions = Roles
                .Select(role => new SelectListItem { Text = role, Value = role })
                .ToList();

            RegisterViewModel registerViewModel = new()
            {
                Options = roleOptions
            };
            return View("Register", registerViewModel);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUserAsync();
            return RedirectToAction("Login");
        }


    }
}
