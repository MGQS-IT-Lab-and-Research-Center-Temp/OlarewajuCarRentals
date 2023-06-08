namespace CarRentals.Models.PaymentCategory
{

    public class PaymentCategoryResponseModel : BaseResponseModel
    {
        public PaymentCategoryViewModel Data { get; set; }
    }
    public class PaymentCategoriesResponseModel : BaseResponseModel
    {
        public List<PaymentCategoryViewModel> Data { get; set; }
    }
}
