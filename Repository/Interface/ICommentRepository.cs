using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface ICommentRepository :IRepository<Comment>
    {
        Comment GetComment(string id);
        Comment GetComment(Expression<Func<Comment, bool>> expression);
        List<Comment> GetAllComments();
    }
}
