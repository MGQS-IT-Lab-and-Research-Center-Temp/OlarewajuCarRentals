namespace CarRentals.Entities
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
        public string CommentText { get; set; }
    }
}
