using CarRentals.Models.CarGallery;
using CarRentals.Models.CarReport;
using CarRentals.Models.Comment;
using Org.BouncyCastle.Asn1.Mozilla;

namespace CarRentals.Models.Car
{
    public class CarViewModel
    {
        public string CarId { get; set; }
        public string Name { get; set; }
        public string CoverImageURL { get; set; }
        public bool AvailabilityStatus { get; set; }
        public decimal Price { get; set; }
        public List<ViewCarGallery> CarGalleries { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public List<CarReportViewModel> CarReports { get; set; }
    }
}
