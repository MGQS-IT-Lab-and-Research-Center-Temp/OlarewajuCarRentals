namespace CarRentals.Models.CarReport
{
    public class CarReportResponseModel : BaseResponseModel
    {
        public CarReportViewModel Data { get; set; }
    }

    public class CarReportsResponseModel : BaseResponseModel
    {
        public List<CarReportViewModel> Data { get; set; }
    }

}
