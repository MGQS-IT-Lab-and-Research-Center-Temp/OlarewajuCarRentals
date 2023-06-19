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
            var car = _context.Cars
                                         .Include(c => c.Comments)
                                         .ThenInclude(c => c.User)
                                          .Include(c => c.CarGalleries)
                                          .SingleOrDefault(expression);
            return car;
        }

        public List<CarCategory> GetCarByCategoryId(string categoryId)
        {
            var car = _context.CarCategories
                           .Include(c => c.Category)
                           .Include(c => c.Car)
                           .Where(c => c.CategoryId.Equals(categoryId))
                           .ToList();

            return car;
        }

        public List<Car> GetCars()
        {
            var cars = _context.Cars
               .Include(uq => uq.CarGalleries)
               .Include(c => c.Bookings)
               .Include(c => c.Comments)
               .ThenInclude(u => u.User)
               .ToList();

            return cars;
        }

        public List<Car> GetCars(Expression<Func<Car, bool>> expression)
        {
            var cars = _context.Cars
                          .Where(expression)
                          .Include(c => c.Bookings)
                          .ThenInclude(bk => bk.User)
                          .Include(u => u.CarGalleries)
                          .Include(c => c.Comments)
                          .ThenInclude(u => u.User)
                          .ToList();

            return cars;
        }
    }
}
