using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Payment GetPaymentById(string id);
        Payment GetPaymentByPaymentReference(string reference);
        List<Payment> GetPayments(Expression<Func<Payment, bool>> expression);
    }
}
