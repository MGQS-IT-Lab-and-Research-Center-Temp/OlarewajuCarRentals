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

        public async Task<List<Comment>> GetAllComments(Expression<Func<Comment, bool>> expression)
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Car)
                .Where(expression)
                .ToListAsync();
            return comments;
        }

        public async Task<Comment> GetComment(string id)
        {
            var comments = await _context.Comments
                              .Include(c => c.User)
                              .Include(c => c.Car)
                              .Where(c => c.Id.Equals(id))
                              .FirstOrDefaultAsync();

            return comments;
        }

        public async Task<Comment> GetComment(Expression<Func<Comment, bool>> expression)
        {
            var comments = await _context.Comments
                          .Where(expression)
                          .Include(u => u.User)
                          .Include(c => c.Car)
                          .FirstOrDefaultAsync();

            return comments;
        }
    }
}
