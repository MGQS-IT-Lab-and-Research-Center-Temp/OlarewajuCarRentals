using CarRentals.Entities;

namespace CarRentals.Models.Payment
{
    public class PaymentViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }  
        public PaymentCategory PaymentCategory { get; set; }
        public int PaymentCategoryId { get; set; }
        public string PaymentReference { get; set; }
    }
}
