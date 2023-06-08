using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CarRentals.Repository.Implementation
{
    public class PaymentCategoryRepository: BaseRepository<PaymentCategory>, IPaymentCategoryRepository
    {
        public PaymentCategoryRepository(CarRentalsContext context) : base(context)
        {
        }
      
        public PaymentCategory GetPaymentCategoryByName(string name)
        {
            var paymentcategory = _context.PaymentCategories.SingleOrDefault(pc => pc.Name == name);
            return paymentcategory;
        }
    }
}
