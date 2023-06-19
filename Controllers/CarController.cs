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
            if (request.CoverPhoto != null)
            {
                string folder = "cars/cover/";
                request.CoverImageUrl = UploadImage(folder, request.CoverPhoto);
            }

            if (request.GalleryFiles != null)
            {
                string folder = "cars/gallery/";

                request.Gallery = new List<CarGalleryModel>();

                foreach (var file in request.GalleryFiles)
                {
                    var gallery = new CarGalleryModel()
                    {
                        Name = file.FileName,
                        URL = UploadImage(folder, file)
                    };
                    request.Gallery.Add(gallery);
                }
            }
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
        public IActionResult GetCarDetail(string id)
        {
            var response = _carService.GetCar(id);
            ViewData["Message"] = response.Message;
            ViewData["Status"] = response.Status;

            return View(response.Data);
        }

        public IActionResult Update(string id)
        {
            var response = _carService.GetCar(id);

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult Update(string id, UpdateCarViewModel request)
        {
            var response = _carService.Update(id, request);

            if (response.Status is false)
            {
                _notyf.Error(response.Message);

                return RedirectToAction("Index", "Home");
            }

            _notyf.Success(response.Message);

            return RedirectToAction("Index", "Question");
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
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

    }
}
