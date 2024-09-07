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



        public async Task<Car> GetCar(Expression<Func<Car, bool>> expression)
        {
            var car =  await _context.Cars
                                         .Include(c => c.Comments)
                                         .ThenInclude(c => c.User)
                                          .Include(c => c.CarGalleries)
                                          .SingleOrDefaultAsync(expression);
            return car;
        }

        public async Task<List<CarCategory>> GetCarByCategoryId(string categoryId)
        {
            var car = await _context.CarCategories
                           .Include(c => c.Category)
                           .Include(c => c.Car)
                           .Where(c => c.CategoryId.Equals(categoryId))
                           .ToListAsync();

            return car;
        }

        public async Task<List<Car>> GetCars()
        {
            var cars =  await _context.Cars
               .Include(uq => uq.CarGalleries)
               .Include(c => c.Bookings)
               .Include(c => c.Comments)
               .ThenInclude(u => u.User)
               .ToListAsync();

            return cars;
        }

        public async Task<List<Car>> GetCars(Expression<Func<Car, bool>> expression)
        {
            var cars =await  _context.Cars
                          .Where(expression)
                          .Include(c => c.Bookings)
                          .ThenInclude(bk => bk.User)
                          .Include(u => u.CarGalleries)
                          .Include(c => c.Comments)
                          .ThenInclude(u => u.User)
                          .ToListAsync();

            return cars;
        }
    }
}
