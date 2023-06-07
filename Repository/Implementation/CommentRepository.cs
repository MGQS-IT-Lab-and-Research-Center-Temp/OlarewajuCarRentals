using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;

namespace CarRentals.Repository.Implementation
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CarRentalsContext context) : base(context)
        {

        }
    }
}
