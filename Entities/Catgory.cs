namespace CarRentals.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CarCategory> CarCategories { get; set; } = new HashSet<CarCategory>();
    }
}
