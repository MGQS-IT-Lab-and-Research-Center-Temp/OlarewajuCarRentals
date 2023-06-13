using CarRentals.Models;
using CarRentals.Models.CarReport;

namespace CarRentals.Services.Interfaces
{
    public interface ICarReportService
    {
        BaseResponseModel CreateCarReport(CreateCarReportViewModel request);
        BaseResponseModel DeleteCarReport(string id);
        BaseResponseModel UpdateCarReport(string id, UpdateCarReportViewModel request);
        CarReportResponseModel GetCarReport(string reportId);
        CarReportsResponseModel GetCarReports(string carId);
    }
}
