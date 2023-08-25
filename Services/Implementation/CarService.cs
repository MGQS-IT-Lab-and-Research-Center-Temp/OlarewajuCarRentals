using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Car;
using CarRentals.Models.Comment;
using CarRentals.Repository.Interfaces;
using CarRentals.Service.Interface;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CarRentals.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<BaseResponseModel> Create(CreateCarViewModel createcarDto)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var platenumexist = await _unitOfWork.Cars.ExistsAsync(c => c.PlateNumber == createcarDto.PlateNumber);
            if (platenumexist)
            {
                response.Message = "PlateNumber already exist already";
                return response;
            }
            if (createcarDto.CoverPhoto != null)
            {
                string folder = "cars/cover/";
                createcarDto.CoverImageUrl = UploadImage(folder, createcarDto.CoverPhoto);
            }

            if (createcarDto.GalleryFiles != null)
            {
                string folder = "cars/gallery/";

                createcarDto.Gallery = new List<CarGalleryModel>();

                foreach (var file in createcarDto.GalleryFiles)
                {
                    var gallery = new CarGalleryModel()
                    {
                        Name = file.FileName,
                        URL = UploadImage(folder, file)
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

            foreach (var file in createcarDto.Gallery)
            {
                newcar.CarGalleries.Add(new CarGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }
            var categories = await  _unitOfWork.Categories.GetAllByIdsAsync(createcarDto.CategoryIds);

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
               await  _unitOfWork.Cars.CreateAsync(newcar);
               await  _unitOfWork.SaveChangesAsync();
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

        public async Task<BaseResponseModel> Delete(string carId)
        {
            var response = new BaseResponseModel();

            Expression<Func<Car, bool>> expression = (q => (q.Id == carId)
                                        && (q.Id == carId
                                        && q.IsDeleted == false
                                        ));

            var carExist = await _unitOfWork.Cars.ExistsAsync(expression);

            if (!carExist)
            {
                response.Message = "car does not exist!";
                return response;
            }


            var car = await _unitOfWork.Cars.GetAsync(carId);

            car.IsDeleted = true;

            try
            {
               await  _unitOfWork.Cars.RemoveAsync(car);
               await _unitOfWork.SaveChangesAsync();
                response.Message = "Car deleted successfully!";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Question delete failed: {ex.Message}";
                return response;
            }
        }

        public async Task<CarsResponseModel> GetAllCar()
        {
            var response = new CarsResponseModel();

            try
            {
                var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                Expression<Func<Car, bool>> expression = car => car.Bookings.Where(bk => bk.UserId == userIdClaim).Any(bk => bk.CarId == car.Id && car.IsDeleted == false);

                var cars = IsInRole ? await _unitOfWork.Cars.GetCars() :await _unitOfWork.Cars.GetCars(expression);

                if (cars.Count == 0)
                {
                    response.Message = "No car found!";
                    return response;
                }

                response.Data = cars
                    .Where(q => q.IsDeleted == false)
                    .Select(car => new CarViewModel
                    {
                        Id = car.Id,
                        Name = car.Name,
                        PlateNumber = car.PlateNumber,
                        CoverImageURL = car.CoverImageUrl,
                        Price = car.Price,
                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }
        public async Task< CarsResponseModel> DisplayCars()
        {
            var response = new CarsResponseModel();

            try
            {

                var cars =  await _unitOfWork.Cars.GetCars(q => q.IsDeleted == false);

                if (cars.Count == 0)
                {
                    response.Message = "No car found!";
                    return response;
                }

                response.Data = cars
                    .Where(q => q.IsDeleted == false)
                    .Select(car => new CarViewModel
                    {
                        Id = car.Id,
                        Name = car.Name,
                        PlateNumber = car.PlateNumber,
                        Price = car.Price,
                        CoverImageURL = car.CoverImageUrl
                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }

        public async Task<CarResponseModel> GetCar(string carId)
        {
            var response = new CarResponseModel();
            var carExist = await  _unitOfWork.Cars.ExistsAsync(q => q.Id == carId && q.IsDeleted == false);
       
            if (!carExist)
            {
                response.Message = $"car with id {carId} does not exist!";
                return response;
            }

            var car = await _unitOfWork.Cars.GetCar(c => c.Id == carId && !c.IsDeleted);

            if (car is null)
            {
                response.Message = "car not found!";
                return response;
            }

            response.Message = "Success";
            response.Status = true;
            response.Data = new CarViewModel
            {
                Id = car.Id,
                Name = car.Name,
                CoverImageURL = car.CoverImageUrl,
                PlateNumber = car.PlateNumber,
                Price = car.Price,
                CarGalleries = car.CarGalleries.Select(cg => new CarGalleryModel()
                {
                    Id = cg.Id,
                    Name = cg.Name,
                    URL = cg.URL
                }).ToList(),
                Comments = car.Comments
                            .Where(c => !c.IsDeleted)
                            .Select(c => new CommentViewModel
                            {
                                Id = c.Id,
                                UserId = c.UserId,
                                CommentText = c.CommentText,
                                UserName = $"{c.User.FirstName} {c.User.LastName}"
                            }).ToList(),
            };

            return response;
        }

        public async Task<CarsResponseModel> GetCarByCategoryId(string categoryId)
        {
            var response = new CarsResponseModel();

            try
            {
                var cars = await _unitOfWork.Cars.GetCarByCategoryId(categoryId);

                if (cars.Count == 0)
                {
                    response.Message = "No car found!";
                    return response;
                }

                response.Data = cars
                                    .Select(cc => new CarViewModel
                                    {
                                        Id = cc.Id,
                                        Name = cc.Car.Name,
                                        PlateNumber = cc.Car.PlateNumber,
                                        Price = cc.Car.Price,
                                        CoverImageURL = cc.Car.CoverImageUrl
                                    }).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }

            return response;
        }

      
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

           await  file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

    }
}
