using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentals.Entities
{
    public class PaymentCategory : BaseEntity
    {
        public string Name { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }
}
