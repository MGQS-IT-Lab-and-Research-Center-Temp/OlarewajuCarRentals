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

        public async Task<User> GetUserById(string id)
        {
            var user = await _context.Users
                .Include(u => u.Bookings)
                .ThenInclude(bk => bk.Car)
                .ThenInclude(c => c.Comments)
                .SingleOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User> GetUser(Expression<Func<User, bool>> expression)
        {
            var user = await _context.Users
                .Include(u => u.Bookings)
                .ThenInclude(bk => bk.Car)
                .ThenInclude(c => c.Comments)
                .Include(c=>c.Role)
                .SingleOrDefaultAsync(expression);
            return user;
        }
        public async Task<List<User>> GetUsers(Expression<Func<User, bool>> expression)
        {
            var users = await _context.Users
                .Include(u => u.Bookings)
                .ThenInclude(bk => bk.Car)
                .ThenInclude(c => c.Comments)
                .Where(expression)
                .ToListAsync();
            return users;
        }
    }
}
