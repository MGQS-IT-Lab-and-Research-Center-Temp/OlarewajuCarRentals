using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<List<Car>> GetCars();
        Task<List<Car>> GetCars(Expression<Func<Car, bool>> expression);
        Task<Car> GetCar(Expression<Func<Car, bool>> expression);
        Task<List<CarCategory>> GetCarByCategoryId(string categoryId);

    }
}
