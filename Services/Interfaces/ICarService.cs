using CarRentals.Models;
using CarRentals.Models.Car;

namespace CarRentals.Service.Interface
{
    public interface ICarService
    {
        CarsResponseModel DisplayCars();
        BaseResponseModel Create(CreateCarViewModel createcarDto);
        BaseResponseModel Delete(string carId);
        BaseResponseModel Update(string carId, UpdateCarViewModel request);
        CarResponseModel GetCar(string carId);
        CarsResponseModel GetAllCar();
    }
}