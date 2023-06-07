using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;

namespace CarRentals.Repository.Implementation
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(CarRentalsContext context) : base(context)
        {
        }
    }
}
