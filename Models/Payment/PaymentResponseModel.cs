namespace CarRentals.Models.Payment
{
    public class PaymentResponseModel : BaseResponseModel
    {
        public PaymentViewModel Data { get; set; }
    }

    public class PaymentsResponseModel : BaseResponseModel
    {
        public List<PaymentViewModel> Data { get; set; }
    }
}
