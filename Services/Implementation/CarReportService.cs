using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.CarReport;
using CarRentals.Repository.Interfaces;
using CarRentals.Services.Interfaces;
using System.Security.Claims;

namespace CarRentals.Services.Implementation
{
    public class CarReportService : ICarReportService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public CarReportService(IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel CreateCarReport(CreateCarReportViewModel request)
        {

            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var reporter = _unitOfWork.Users.Get(userIdClaim);
            var car = _unitOfWork.Cars.Get(request.CarId);
            var bookings = _unitOfWork.Bookings.GetAllBookings(bk => bk.CarId == request.CarId);

            if (reporter is null)
            {
                response.Message = "User not found!";
                return response;
            }

            if (car is null)
            {
                response.Message = "car not found!";
                return response;
            }
            foreach (var booking in bookings)
            {
                var bookeduser = _unitOfWork.Users.Get(booking.UserId);
                if (bookeduser != reporter)
                {
                    response.Message = "You cannot comment not on this car!!";
                    return response;
                }
            }

            var carReport = new CarReport
            {
                UserId = reporter.Id,
                User = reporter,
                CarId = car.Id,
                Car = car,
                AdditionalComment = request.AdditionalComment,
                CreatedBy = createdBy
            };
            try
            {

                _unitOfWork.CarReports.Create(carReport);
                response.Status = true;
                response.Message = "Report created successfully!";
                _unitOfWork.SaveChanges();
                return response;

            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.StackTrace}";
                return response;
            }
        }

        public BaseResponseModel DeleteCarReport(string id)
        {

            var response = new BaseResponseModel();

            var isCarReportExist = _unitOfWork.CarReports.Exists(c => c.Id == id);

            if (!isCarReportExist)
            {
                response.Message = "Report does not exist!";
                return response;
            }

            var carReport = _unitOfWork.CarReports.Get(id);
            carReport.IsDeleted = true;

            try
            {
                _unitOfWork.CarReports.Update(carReport);
            }
            catch (Exception ex)
            {
                response.Message = $"Question report delete failed: {ex.Message}";
                return response;
            }

            response.Status = true;
            response.Message = "Question report deleted successfully!";
            _unitOfWork.SaveChanges();
            return response;
        }

        public CarReportResponseModel GetCarReport(string reportId)
        {
            var response = new CarReportResponseModel();

            var isCarReportExist = _unitOfWork.CarReports.Exists(c => c.Id == reportId);

            if (!isCarReportExist)
            {
                response.Message = $"Report with id {reportId} does not exist!";
                return response;
            }

            var carReport = _unitOfWork.CarReports.GetCarReport(reportId);

            response.Message = "Success";
            response.Status = true;

            response.Data = new CarReportViewModel
            {
                Id = reportId,
                AdditionalComment = carReport.AdditionalComment,
                CarId = carReport.Car.Id,
                CarReporter = $"{carReport.User.FirstName}{carReport.User.LastName}",
            };

            return response;
        }

        public CarReportsResponseModel GetCarReports(string carId)
        {
            var response = new CarReportsResponseModel();

            try
            {
                var carReport = _unitOfWork.CarReports.GetCarReports(carId);

                response.Data = carReport
                    .Select(cr => new CarReportViewModel
                    {
                        Id = cr.Id,
                        CarId = cr.CarId,
                        CarReporter = $"{cr.User.FirstName}{cr.User.LastName}",
                        AdditionalComment = cr.AdditionalComment,
                    }).ToList();

                response.Status = true;
                response.Message = "Success";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured: {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel UpdateCarReport(string id, UpdateCarReportViewModel request)
        {
            var response = new BaseResponseModel();

            var carReportExist = _unitOfWork.CarReports.Exists(c => c.Id == id);

            if (!carReportExist)
            {
                response.Message = "Car report does not exist!";
                return response;
            }

            var carReport = _unitOfWork.CarReports.Get(id);

            carReport.AdditionalComment = request.AdditionalComment;

            try
            {
                _unitOfWork.CarReports.Update(carReport);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the Car report: {ex.Message}";
                return response;
            }

            response.Message = "Car report updated successfully!";

            return response;
        }
    }
}
