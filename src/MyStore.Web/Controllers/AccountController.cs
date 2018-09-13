using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStore.Core.Domain;
using MyStore.Services.Users;
using MyStore.Web.Framework;
using MyStore.Web.Models;

namespace MyStore.Web.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IAuthenticator _authenticator;
        private readonly IUserService _userService;

        public AccountController(IAuthenticator authenticator,
            IUserService userService)
        {
            _authenticator = authenticator;
            _userService = userService;
        }
        
        [HttpGet("sign-up")]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();

            return View(viewModel);
        }
        
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _userService
                .SignUpAsync(viewModel.Username, viewModel.Password, viewModel.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _userService.SignInAsync(viewModel.Username, viewModel.Password);
            var role = await _userService.GetRoleAsync(viewModel.Username);
            await _authenticator.SignInAsync(viewModel.Username, role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticator.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Content(User.Identity.Name);
        }
        
        //sign-up GET + POST
        // Zwroc widok tworzenia konta
        // SignUpViewModel (role: user, admin)
        // Compare password
        // Zarejestruj -> POST sign-up
    }
}