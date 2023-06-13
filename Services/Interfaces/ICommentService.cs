using CarRentals.Models.Comment;
using CarRentals.Models;
using static CarRentals.Models.Comment.CommentResponse;

namespace CarRentals.Services.Interfaces
{
    public interface ICommentService
    {
        BaseResponseModel CreateComment(CreateCommentViewModel request);
        BaseResponseModel DeleteComment(string commentId);
        BaseResponseModel UpdateComment(string commentId, UpdateCommentViewModel request);
        CommentResponseModel GetComment(string commentId);
        CommentsResponseModel GetAllComment();
    }
}
