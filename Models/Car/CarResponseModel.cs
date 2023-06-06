namespace CarRentals.Models.Car
{
    public class CarResponseModel : BaseResponseModel
    {

        public CarViewModel Data { get; set; }
    }
    public class CarsResponseModel : BaseResponseModel
    {
        public List<CarViewModel> Data { get; set; }
    }
}
