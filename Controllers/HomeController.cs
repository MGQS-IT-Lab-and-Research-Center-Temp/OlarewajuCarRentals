using AspNetCoreHero.ToastNotification.Abstractions;
using CarRentals.Models.Auth;
using CarRentals.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CarRentals.Service.Interface;
using CarRentals.ActionFilter;

namespace CarRentals.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICarService _carService;
        private readonly INotyfService _notyf;

        public HomeController(
            IUserService userService,
            ICarService carService,
            INotyfService notyf)
        {
            _userService = userService;
            _carService = carService;
            _notyf = notyf;
        }


        public IActionResult Index()
        {
            var cars = _carService.DisplayCars();
            ViewData["Message"] = cars.Message;
            ViewData["Status"] = cars.Status;

            return View(cars.Data);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            var response = _userService.Register(model);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return View(model);
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }

        [RedirectIfAuthenticated]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var response = _userService.Login(model);
            var user = response.Data;

            if (response.Status == false)
            {
                _notyf.Error(response.Message);

                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authenticationProperties = new AuthenticationProperties();

            var principal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            _notyf.Success(response.Message);

            if (user.RoleName == "Admin")
            {
                return RedirectToAction("AdminDashboard", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _notyf.Success("You have successfully signed out!");
            return RedirectToAction("Login", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
    }
}