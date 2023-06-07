using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Repository.Implementation
{
    public class CarReportRepository : BaseRepository<CarReport>, ICarReportRepository
    {
        public CarReportRepository(CarRentalsContext context) : base(context)
        {
        }

        public CarReport GetCarReport(string id)
        {
            var carReport = _context.CarReports
                                   .Include(u => u.User)
                                   .Include(c => c.Car)
                                   .Where(cr => cr.Id.Equals(id))
                                   .FirstOrDefault();

            return carReport;
        }

        public List<CarReport> GetCarReports()
        {
            var carReport = _context.CarReports
                .Include(cr => cr.User)
                .Include(cr => cr.Car)
                .ToList();
            return carReport;
        }
    }
}
