using AspNetCoreHero.ToastNotification.Abstractions;
using CarRentals.Models;
using CarRentals.Models.Car;
using CarRentals.Service.Interface;
using CarRentals.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentals.Controllers
{

    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;


        public CarController(
            ICarService carService,
            ICategoryService categoryService,
            IHttpContextAccessor httpContextAccessor,
            INotyfService notyf
,
            IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
            _notyf = notyf;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var cars = _carService.GetAllCar();
            ViewData["Message"] = cars.Message;
            ViewData["Status"] = cars.Status;

            return View(cars.Data);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryService.SelectCategories();
            ViewData["Message"] = "";
            ViewData["Status"] = false;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCarViewModel request)
        {
          
            var response = _carService.Create(request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Car");
        }


        public IActionResult GetCarByCategory(string id)
        {
            var response = _carService.GetCarByCategoryId(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }
        [AllowAnonymous]
        public IActionResult GetCarDetail(string id)
        {
            var response = _carService.GetCar(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

      

        [HttpPost]
        public IActionResult DeleteCar([FromRoute] string id)
        {
            var response = _carService.Delete(id);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);
                return View();
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Home");
        }
       

    }
}
