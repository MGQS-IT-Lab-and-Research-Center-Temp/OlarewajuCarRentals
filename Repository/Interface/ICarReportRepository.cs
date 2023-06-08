using CarRentals.Entities;

namespace CarRentals.Repository.Interface
{
    public interface ICarReportRepository  :IRepository<CarReport>
    {
        List<CarReport> GetCarReports();
        CarReport GetCarReport(string id);
    }
}
