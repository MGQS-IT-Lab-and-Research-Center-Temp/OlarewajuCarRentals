using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRentals.Repository.Implementation
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(CarRentalsContext context) : base(context)
        {
        }
        public Payment GetPaymentById(string id)
        {
            var payment = _context.Payments
                .Include(p => p.User)
                .Include(p => p.Car)
                .Include(p => p.PaymentCategory)
                .SingleOrDefault(p => p.Id == id);


            return payment;

        }
        public Payment GetPaymentByPaymentReference(string reference)
        {
            var payment = _context.Payments
                .Include(c => c.User)
                .Include(c => c.Car)
                .Include(c => c.PaymentCategory)
                .SingleOrDefault(c => c.PaymentReference == reference);
            return payment;
        }

        public  List<Payment> GetPayments(Expression<Func<Payment, bool>> expression)
        {
           var payments = _context.Payments
                .Include(c => c.User)
                .Include(c => c.Car)
                .Include(c => c.PaymentCategory)
                .Where(expression)
                .ToList();

            return payments;
        }
    }
}
