using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRentals.Repository.Implementation
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(CarRentalsContext context) : base(context)
        {
        }



        public Car GetCar(Expression<Func<Car, bool>> expression)
        {
            var car = _context.Cars.Include(c => c.Comments)
                                          .ThenInclude(c => c.User)
                                          .Include(c => c.CarReports)
                                          .ThenInclude(cr => cr.User)
                                          .Include(c => c.CarGalleries)
                                          .SingleOrDefault(expression);
            return car;
        }

        public List<Car> GetCars()
        {
            var cars = _context.Cars
               .Include(uq => uq.CarGalleries)
               .Include(c => c.Comments)
               .ThenInclude(u => u.User)
               .Include(qr => qr.CarReports)
                .ThenInclude(cr => cr.User)
               .ToList();

            return cars;
        }

        public List<Car> GetCars(Expression<Func<Car, bool>> expression)
        {
            var cars = _context.Cars
                          .Where(expression)
                          .Include(u => u.CarGalleries)
                          .Include(c => c.Comments)
                          .ThenInclude(u => u.User)
                          .Include(qr => qr.CarReports)
                          .ToList();

            return cars;
        }
    }
}
