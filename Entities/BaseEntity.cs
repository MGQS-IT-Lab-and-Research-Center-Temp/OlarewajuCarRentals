using CarRentals.Entities;
using MassTransit;

namespace CarRentals.Entities
{
    public abstract class BaseEntity : ISoftDeletable, IAuditBase
    
    {
        public string Id { get; set; } = NewId.Next().ToSequentialGuid().ToString();
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
