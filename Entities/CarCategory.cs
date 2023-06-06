namespace CarRentals.Entities
{
    public class CarCategory : BaseEntity
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
    }
}