namespace CarRentals.Entities
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }
}
