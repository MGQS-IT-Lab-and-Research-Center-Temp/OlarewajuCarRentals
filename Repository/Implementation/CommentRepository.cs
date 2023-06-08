using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRentals.Repository.Implementation
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CarRentalsContext context) : base(context)
        {

        }

        public List<Comment> GetAllComments(Expression<Func<Comment, bool>> expression)
        {
            var comments = _context.Comments
                .Include(c => c.User)
                .Include(c => c.Car)
                .Where(expression)
                .ToList();
            return comments;
        }

        public Comment GetComment(string id)
        {
            var comments = _context.Comments
                              .Include(c => c.User)
                              .Include(c => c.Car)
                              .Where(c => c.Id.Equals(id))
                              .FirstOrDefault();

            return comments;
        }

        public Comment GetComment(Expression<Func<Comment, bool>> expression)
        {
            var comments = _context.Comments
                          .Where(expression)
                          .Include(u => u.User)
                          .Include(c => c.Car)
                          .FirstOrDefault();

            return comments;
        }
    }
}
