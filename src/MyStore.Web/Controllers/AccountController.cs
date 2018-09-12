using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Domain;
using MyStore.Web.Models;

namespace MyStore.Web.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private static readonly List<User> _users = new List<User>();

        [HttpGet("sign-up")]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();

            return View(viewModel);
        }
        
        [HttpPost("sign-up")]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _users.Add(new User
            {
                Id = Guid.NewGuid(),
                Username = viewModel.Username,
                Password = viewModel.Password,
                Role = viewModel.Role
            });

            return RedirectToAction("Index", "Home");
        }

        
        //sign-up GET + POST
        // Zwroc widok tworzenia konta
        // SignUpViewModel (role: user, admin)
        // Compare password
        // Zarejestruj -> POST sign-up
    }
}