using System.ComponentModel.DataAnnotations;

namespace CarRentals.Models.CarReport
{
    public class CreateCarReportViewModel
    {
        public string UserId { get; set; }
        public string CarId { get; set; }
        [Required(ErrorMessage = " Comment text required")]
        [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
        [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
        public string AdditionalComment { get; set; }
    }
}
