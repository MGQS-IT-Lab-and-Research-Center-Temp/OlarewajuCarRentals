using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface ICarRepository : IRepository<Car>
    {
        List<Car> GetCars();
        List<Car> GetCars(Expression<Func<Car, bool>> expression);
        Car GetCar(Expression<Func<Car, bool>> expression);
        List<CarCategory> GetCarByCategoryId(string categoryId);

    }
}
