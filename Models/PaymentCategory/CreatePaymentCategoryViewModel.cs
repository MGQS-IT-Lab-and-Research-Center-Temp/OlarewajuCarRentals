using DaySpring.Dtos;
using DaySpring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySpring.ViewModels
{
    public class CreatePaymentCategoryRequestModel
    {
        public string Name { get; set; }
    }

    public class UpdatePaymentCategoryRequestModel
    {
        public string Name { get; set; }
    }

    public class PaymentCategoryResponseModel : BaseResponse
    {
        public PaymentCategoryModel Data { get; set; }
    }
    public class PaymentCategoriesResponseModel : BaseResponse
    {
        public List<PaymentCategoryModel> Data { get; set; }
    }

    public class GetPaymentByDate
    {
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public List<int> PaymentCategoryIds { get; set; }
    }
}
