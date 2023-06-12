using CarRentals.Context.EntityConfiguration;
using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Car;
using CarRentals.Repository.Interfaces;
using CarRentals.Service.Interface;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;

namespace CarRentals.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateCarViewModel createcarDto)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (createcarDto.CoverPhoto != null)
            {
                string folder = "books/cover/";
                createcarDto.CoverImageUrl =  UploadImage(folder, createcarDto.CoverPhoto);
            }

            if (createcarDto.GalleryFiles != null)
            {
                string folder = "books/gallery/";

                createcarDto.Gallery = new List<CarGalleryModel>();

                foreach (var file in createcarDto.GalleryFiles)
                {
                    var gallery = new CarGalleryModel()
                    {
                        Name = file.FileName,
                        URL =  UploadImage(folder, file)
                    };
                    createcarDto.Gallery.Add(gallery);
                }
            }

            var newcar = new Car
            {
                Name = createcarDto.Name,
                CreatedBy = createdBy,
                PlateNumber = createcarDto.PlateNumber,
                CoverImageUrl = createcarDto.CoverImageUrl,
                AailabilityStaus = true,
                Price = createcarDto.Price,
            };
             newcar.CarGalleries = new List<CarGallery>();

            foreach (var file in newcar.CarGalleries)
            {
                newcar.CarGalleries.Add(new CarGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }
            var categories = _unitOfWork.Categories.GetAllByIds(createcarDto.CategoryIds);

            var carCategories = new HashSet<CarCategory>();

            foreach (var category in categories)
            {
                var carCategory = new CarCategory
                {
                    CategoryId = category.Id,
                    CarId = newcar.Id,
                    Category = category,
                    Car = newcar,
                    CreatedBy = createdBy
                };

                carCategories.Add(carCategory);
            }

            newcar.CarCategories = carCategories;

            try
            {
                _unitOfWork.Cars.Create(newcar);
                _unitOfWork.SaveChanges();
                response.Message = "Car created successfully!";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create car: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Delete(string carId)
        {
            throw new NotImplementedException();
        }

        public CarsResponseModel GetAllCar()
        {
            throw new NotImplementedException();
        }

        public CarResponseModel GetCar(string carId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Update(string carId, UpdateCarViewModel request)
        {
            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var carExist = _unitOfWork.Cars.Exists(c => c.Id == carId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (!carExist)
            {
                response.Message = "car does not exist!";
                return response;
            }

            var car = _unitOfWork.Cars.Get(carId);
            car.Name = request.Name;
            if (request.CoverPhoto != null)
            {
                string folder = "books/cover/";
                request.CoverImageUrl = UploadImage(folder, request.CoverPhoto);
            }
            car.CoverImageUrl = request.CoverImageUrl;
            if (request.GalleryFiles != null)
            {
                string folder = "books/gallery/";

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
            car.CarGalleries = new List<CarGallery>();

            foreach (var file in car.CarGalleries)
            {
                car.CarGalleries.Add(new CarGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }
             car.ModifiedBy = modifiedBy;

            try
            {
                _unitOfWork.Cars.Update(car);
                _unitOfWork.SaveChanges();
                response.Message = "Car updated successfully!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the Question: {ex.Message}";
                return response;
            }
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
