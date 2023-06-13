using CarRentals.Entities;

namespace CarRentals.Repository.Interface
{
    public interface ICarReportRepository  :IRepository<CarReport>
    {
        List<CarReport> GetCarReports(string carId);
        CarReport GetCarReport(string id);
    }
}
