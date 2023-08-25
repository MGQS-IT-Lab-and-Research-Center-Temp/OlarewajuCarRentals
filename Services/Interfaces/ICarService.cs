using CarRentals.Models;
using CarRentals.Models.Car;

namespace CarRentals.Service.Interface;

public interface ICarService
{
    Task<CarsResponseModel> DisplayCars();
    Task<BaseResponseModel> Create(CreateCarViewModel createcarDto);
    Task<BaseResponseModel> Delete(string carId);
    Task<CarResponseModel> GetCar(string carId);
    Task<CarsResponseModel> GetCarByCategoryId(string categoryId);
    Task<CarsResponseModel> GetAllCar();
}