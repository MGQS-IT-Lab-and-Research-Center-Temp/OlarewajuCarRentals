using CarRentals.Context;
using CarRentals.Entities;
using CarRentals.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Linq.Expressions;

namespace CarRentals.Repository.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CarRentalsContext context) : base(context)
        {
        }

        public User GetUserById(string id)
        {
            var user = _context.Users
                .Include(u => u.Bookings)
                .ThenInclude(bk => bk.Car)
                .ThenInclude(c => c.Comments)
                .SingleOrDefault(u => u.Id == id);
            return user;
        }
        public User GetUser(Expression<Func<User, bool>> expression)
        {
            var user = _context.Users
                .Include(u => u.Bookings)
                .ThenInclude(bk => bk.Car)
                .ThenInclude(c => c.Comments)
                .SingleOrDefault(expression);
            return user;
        }
        public List<User> GetUsers(Expression<Func<User, bool>> expression)
        {
            var users = _context.Users
                .Include(u => u.Bookings)
                .ThenInclude(bk => bk.Car)
                .ThenInclude(c => c.Comments)
                .Where(expression)
                .ToList();
            return users;
        }
    }
}
