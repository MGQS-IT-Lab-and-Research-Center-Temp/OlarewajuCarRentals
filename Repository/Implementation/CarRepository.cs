﻿using CarRentals.Context;
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
            throw new NotImplementedException();
        }

        public List<Car> GetCars(Expression<Func<Car, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}