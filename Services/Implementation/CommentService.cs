using CarRentals.Entities;
using CarRentals.Models;
using CarRentals.Models.Comment;
using CarRentals.Repository.Interfaces;
using CarRentals.Services.Interfaces;
using System.Security.Claims;

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

        public BaseResponseModel CreateComment(CreateCommentViewModel request)
        {
            var response = new BaseResponseModel();
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if(user is null)
            {
                response.Message = "User not found";
                return response;
            }

            var car = _unitOfWork.Cars.Get(request.CarId);

            if(car is null)
            {
                response.Message = "car not found";
                return response;
            }
           if(car.Bookings.)
            {

            }

            if (string.IsNullOrWhiteSpace(request.CommentText))
            {
                response.Message = "Comment text is required!";
                return response;
            }

            var comment = new Comment
            {
                UserId = user.Id,
                User = user,
                QuestionId = question.Id,
                Question = question,
                CommentText = request.CommentText,
                CreatedBy = createdBy,
            };

            try
            {
                _unitOfWork.Comments.Create(comment);
                _unitOfWork.SaveChanges();
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

        public BaseResponseModel DeleteComment(string commentId)
        {
            throw new NotImplementedException();
        }

        public CommentResponse.CommentsResponseModel GetAllComment()
        {
            throw new NotImplementedException();
        }

        public CommentResponse.CommentResponseModel GetComment(string commentId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel UpdateComment(string commentId, UpdateCommentViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}
