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
            var LoggedInuser = _unitOfWork.Users.Get(userIdClaim);
            var bookings = _unitOfWork.Bookings.GetAllBookings(bk => bk.CarId == request.CarId);
            if(LoggedInuser is null)
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
            foreach (var booking in bookings)
            {
                var bookeduser = _unitOfWork.Users.Get(booking.UserId);
                if (bookeduser != LoggedInuser)
                {
                    response.Message = "You cannot comment not on this car!!";
                    return response;
                }
            }

            if (string.IsNullOrWhiteSpace(request.CommentText))
            {
                response.Message = "Comment text is required!";
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
            var response = new BaseResponseModel();
            var commentexist = _unitOfWork.Comments.Exists(c => c.Id == commentId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (!commentexist)
            {
                response.Message = "Comment  does not exist.";
                return response;
            }

            var comment = _unitOfWork.Comments.Get(commentId);
            if (comment.UserId != user.Id)
            {
                response.Message = "You can not delete this Comment!";
                return response;
            }

            comment.IsDeleted = true;

            try
            {
                _unitOfWork.Comments.Update(comment);
                _unitOfWork.SaveChanges();
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

            var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var commentexist = _unitOfWork.Comments.Exists(c => c.Id == commentId);
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _unitOfWork.Users.Get(userIdClaim);

            if (!commentexist)
            {
                response.Message = "Comment  does not exist.";
                return response;
            }
            var comment = _unitOfWork.Comments.Get(commentId);

            if (comment.UserId != userIdClaim)
            {
                response.Message = "You can not update this comment";
                return response;
            }

            comment.CommentText = request.CommentText;
            comment.ModifiedBy = modifiedBy;

            try
            {
                _unitOfWork.Comments.Update(comment);
                _unitOfWork.SaveChanges();
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
