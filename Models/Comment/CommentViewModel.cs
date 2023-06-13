using CarRentals.Models.CarReport;

namespace CarRentals.Models.Comment;

public class CommentViewModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string CarId { get; set; }
    public string CommentText { get; set; }
    public string UserName { get; set; }
}
