using CarRentals.Entities;

namespace CarRentals.Repository.Interface
{
    public interface IPaymentCategoryRepository : IRepository<PaymentCategory>
    {
        PaymentCategory GetPaymentCategoryByName(string name);
    }
}
