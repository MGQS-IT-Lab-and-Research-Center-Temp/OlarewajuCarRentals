using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Comment;
using CarRentals.Repository.Interfaces;
using CarRentals.Services.Interfaces;
using System.Linq.Expressions;
using System.Security.Claims;
using static CarRentals.Models.Comment.CommentResponse;

namespace CarRentals.Services.Implementation
{
    public class CommentService : ICommentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponseModel> CreateComment(CreateCommentViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var LoggedInuser = await _unitOfWork.Users.GetAsync(userIdClaim);


            if (LoggedInuser is null)
            {
                response.Message = "User not found";
                return response;
            }

            var car = await _unitOfWork.Cars.GetAsync(request.CarId);

            Expression<Func<User, bool>> expression = user => user.Bookings.Where(bk => bk.CarId == request.CarId).Any(bk => bk.UserId == user.Id);

            var bookedcarusers = await _unitOfWork.Users.GetUsers(expression);
            if (car is null)
            {
                response.Message = "car not found";
                return response;
            }

            if (string.IsNullOrWhiteSpace(request.CommentText))
            {
                response.Message = "Comment text is required!";
                return response;
            }
            if (!bookedcarusers.Contains(LoggedInuser))
            {
                response.Message = " You cannot comment on this car ";
                return response;
            }
            var comment = new Comment
            {
                UserId = LoggedInuser.Id,
                User = LoggedInuser,
                CarId = car.Id,
                Car = car,
                CommentText = request.CommentText,
                CreatedBy = createdBy,
            };

            try
            {
                await _unitOfWork.Comments.CreateAsync(comment);
                await _unitOfWork.SaveChangesAsync();
                response.Status = true;
                response.Message = "Comment  created successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to create comment . {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponseModel> DeleteComment(string commentId)
        {
            var response = new BaseResponseModel();
            var commentexist = await _unitOfWork.Comments.ExistsAsync(c => c.Id == commentId);
            var IsInRole = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.Users.GetAsync(userIdClaim);

            if (!commentexist)
            {
                response.Message = "Comment  does not exist.";
                return response;
            }

            var comment = await _unitOfWork.Comments.GetAsync(commentId);
            if (comment.UserId != user.Id && !IsInRole)
            {
                response.Message = "You can not delete this Comment!";
                return response;
            }

            comment.IsDeleted = true;

            try
            {
                await _unitOfWork.Comments.RemoveAsync(comment);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Message = $"Comment  delete failed {ex.Message}";
                return response;
            }

            response.Status = true;
            response.Message = "Comment  deleted successfully.";
            return response;
        }

        public async Task<CommentsResponseModel> GetAllComment()
        {
            var response = new CommentsResponseModel();

            var comment = await _unitOfWork.Comments.GetAllComments(c => c.IsDeleted == false);

            if (comment.Count == 0)
            {
                response.Message = "No comments yet!";
                return response;
            }

            response.Data = comment
                    .Select(comment => new CommentViewModel
                    {
                        Id = comment.Id,
                        CarId = comment.CarId,
                        UserId = comment.UserId,
                        UserName = $"{comment.User.FirstName} {comment.User.LastName}",
                        CommentText = comment.CommentText
                    }).ToList();

            response.Status = true;
            response.Message = "Success";

            return response;
        }

        public async Task<CommentResponseModel> GetComment(string commentId)
        {
            var response = new CommentResponseModel();
            var commentexist = await _unitOfWork.Comments.ExistsAsync(c => c.Id == commentId);

            if (!commentexist)
            {
                response.Message = $"Comment does not exist.";
                return response;
            }

            var comment = await _unitOfWork.Comments.GetComment(commentId);

            response.Message = "Success";
            response.Status = true;
            response.Data = new CommentViewModel
            {
                Id = comment.Id,
                CarId = comment.CarId,
                UserId = comment.UserId,
                UserName = $"{comment.User.FirstName} {comment.User.LastName}",
                CommentText = comment.CommentText,
            };

            return response;
        }

        public async Task<BaseResponseModel> UpdateComment(string commentId, UpdateCommentViewModel request)
        {

            var response = new BaseResponseModel();
            var commentexist = await _unitOfWork.Comments.ExistsAsync(c => c.Id == commentId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.Users.GetAsync(userIdClaim);

            if (!commentexist)
            {
                response.Message = "Comment  does not exist.";
                return response;
            }
            var comment = await _unitOfWork.Comments.GetAsync(commentId);

            if (comment.UserId != userIdClaim)
            {
                response.Message = "You can not update this comment";
                return response;
            }

            comment.CommentText = request.CommentText;

            try
            {
                await _unitOfWork.Comments.UpdateAsync(comment);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the comment : {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Comment  updated successfully.";

            return response;
        }
    }
}
