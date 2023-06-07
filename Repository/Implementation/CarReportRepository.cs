using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;

namespace CarRentals.Repository.Implementation
{
    public class CarReportRepository : BaseRepository<CarReport>, ICarReportRepository
    {
        public CarReportRepository(CarRentalsContext context) : base(context)
        {
        }
    }
}
