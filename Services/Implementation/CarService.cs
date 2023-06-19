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
        public BaseResponseModel Create(CreateCarViewModel createcarDto)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;




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
            var response = new BaseResponseModel();

            Expression<Func<Car, bool>> expression = (q => (q.Id == carId)
                                        && (q.Id == carId
                                        && q.IsDeleted == false
                                        ));

            var carExist = _unitOfWork.Cars.Exists(expression);

            if (!carExist)
            {
                response.Message = "car does not exist!";
                return response;
            }


            var car = _unitOfWork.Cars.Get(carId);

            car.IsDeleted = true;

            try
            {
                _unitOfWork.Cars.Update(car);
                _unitOfWork.SaveChanges();
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

        public CarsResponseModel GetAllCar()
        {
            var response = new CarsResponseModel();

            try
            {
                var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
                var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                Expression<Func<Car, bool>> expression = car => car.Bookings.Where(bk => bk.UserId == userIdClaim).Any(bk => bk.CarId == car.Id);

                var cars = IsInRole ? _unitOfWork.Cars.GetCars(c=>c.IsDeleted == false) : _unitOfWork.Cars.GetCars(expression);

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
                        CarGalleries = car.CarGalleries.Select(cg => new CarGalleryModel()
                        {
                            Id = cg.Id,
                            Name = cg.Name,
                            URL = cg.URL
                        }).ToList(),
                        Comments = car.Comments
                        .Select(comment => new CommentViewModel
                        {
                            Id = comment.Id,
                            CommentText = comment.CommentText,
                            UserName = $"{comment.User.FirstName}  {comment.User.LastName}",
                        }).ToList(),
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
        public CarsResponseModel DisplayCars()
        {
            var response = new CarsResponseModel();

            try
            {

                var cars = _unitOfWork.Cars.GetCars(q => q.IsDeleted == false);

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
                        CoverImageURL = car.CoverImageUrl,
                        CarGalleries = car.CarGalleries.Select(cg => new CarGalleryModel()
                        {
                            Id = cg.Id,
                            Name = cg.Name,
                            URL = cg.URL
                        }).ToList(),
                        Comments = car.Comments.Where(q => q.IsDeleted == false)
                        .Select(comment => new CommentViewModel
                        {
                            Id = comment.Id,
                            CommentText = comment.CommentText,
                            UserName = $"{comment.User.FirstName}  {comment.User.LastName}",
                        }).ToList(),
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

        public CarResponseModel GetCar(string carId)
        {
            var response = new CarResponseModel();
            var carExist = _unitOfWork.Cars.Exists(q => q.Id == carId && q.IsDeleted == false);
            //var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            //var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var car = new Car();

            if (!carExist)
            {
                response.Message = $"car with id {carId} does not exist!";
                return response;
            }

            car = _unitOfWork.Cars.GetCar(c => c.Id == carId && !c.IsDeleted );


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

        public CarsResponseModel GetCarByCategoryId(string categoryId)
        {
            var response = new CarsResponseModel();

            try
            {
                var cars = _unitOfWork.Cars.GetCarByCategoryId(categoryId);

                if (cars.Count == 0)
                {
                    response.Message = "No car found!";
                    return response;
                }

                response.Data = cars.Where(c=>c.IsDeleted )
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
            //if (request.CoverPhoto != null)
            //{
            //    string folder = "cars/cover/";
            //    request.CoverImageUrl = UploadImage(folder, request.CoverPhoto);
            //}
            //car.CoverImageUrl = request.CoverImageUrl;
            //if (request.GalleryFiles != null)
            //{
            //    string folder = "cars/gallery/";

            //    request.Gallery = new List<CarGalleryModel>();

            //    foreach (var file in request.GalleryFiles)
            //    {
            //        var gallery = new CarGalleryModel()
            //        {
            //            Name = file.FileName,
            //            URL = UploadImage(folder, file)
            //        };
            //        request.Gallery.Add(gallery);
            //    }
            //}
            //car.CarGalleries = new List<CarGallery>();

            //foreach (var file in car.CarGalleries)
            //{
            //    car.CarGalleries.Add(new CarGallery()
            //    {
            //        Name = file.Name,
            //        URL = file.URL
            //    });
            //}
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

    }
}
