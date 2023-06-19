using CarRentals.Models;
using CarRentals.Models.Car;

namespace CarRentals.Service.Interface
{
    public interface ICarService
    {
        CarsResponseModel DisplayCars();
        BaseResponseModel Create(CreateCarViewModel createcarDto);
        BaseResponseModel Delete(string carId);
        CarResponseModel GetCar(string carId);
        CarsResponseModel GetCarByCategoryId(string categoryId);
        CarsResponseModel GetAllCar();
    }
}