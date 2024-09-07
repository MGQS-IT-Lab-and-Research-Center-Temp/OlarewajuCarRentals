using CarRentals.Models.Comment;
using CarRentals.Models;
using static CarRentals.Models.Comment.CommentResponse;

namespace CarRentals.Services.Interfaces
{
    public interface ICommentService
    {
        Task<BaseResponseModel> CreateComment(CreateCommentViewModel request);
        Task<BaseResponseModel> DeleteComment(string commentId);
        Task<BaseResponseModel> UpdateComment(string commentId, UpdateCommentViewModel request);
        Task<CommentResponseModel> GetComment(string commentId);
        Task<CommentsResponseModel> GetAllComment();
    }
}
