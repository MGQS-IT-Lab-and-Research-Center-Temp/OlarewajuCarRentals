using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface ICommentRepository :IRepository<Comment>
    {
        Task<Comment> GetComment(string id);
        Task<Comment> GetComment(Expression<Func<Comment, bool>> expression);
       Task<List<Comment>> GetAllComments(Expression<Func<Comment, bool>> expression);
    }
}
