using CarRentals.Models.CarReport;

namespace CarRentals.Models.Comment;

public class CommentViewModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string QuestionId { get; set; }
    public string CommentText { get; set; }
    public string UserName { get; set; }
    public List<CarReportViewModel> CommentReports = new();
}
