using System.ComponentModel.DataAnnotations;

namespace CarRentals.Models.CarReport
{
    public class UpdateCarReportViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        [Required(ErrorMessage = " Comment text required")]
        [MinLength(20, ErrorMessage = "Minimum of 20 character required")]
        [MaxLength(150, ErrorMessage = "Maximum of 150 character required")]
        public string AdditionalComment { get; set; }
    }
}
